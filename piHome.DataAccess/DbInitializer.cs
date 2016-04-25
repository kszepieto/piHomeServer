using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Conventions;
using piHome.Models.Entities.Auth;
using piHome.Models.Entities.Circuits;
using piHome.Models.Enums;
using piHome.Utils;

namespace piHome.DataAccess
{
    public static class DbInitializer
    {
        public static async Task Initialize(IDbContext dbContext)
        {
            RegisterConventions();
            await EnsureIndexes(dbContext);
            await Seed(dbContext);
        }

        private static void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }

        private static async Task EnsureIndexes(IDbContext dbContext)
        {
            var clientIdField = new StringFieldDefinition<ClientEntity>(ExpressionsHelper.GetPropertyName<ClientEntity>(c => c.ClientId));
            var clientIdIndex = new IndexKeysDefinitionBuilder<ClientEntity>().Ascending(clientIdField);
            await dbContext.Clients.Indexes.CreateOneAsync(clientIdIndex, new CreateIndexOptions() { Unique = true });

            var refreshTokenIdField = new StringFieldDefinition<RefreshTokenEntity>(ExpressionsHelper.GetPropertyName<RefreshTokenEntity>(c => c.RefreshTokenId));
            var refreshTokenIdIndex = new IndexKeysDefinitionBuilder<RefreshTokenEntity>().Ascending(refreshTokenIdField);
            await dbContext.RefreshTokens.Indexes.CreateOneAsync(refreshTokenIdIndex, new CreateIndexOptions() { Unique = true });
        }

        private static async Task Seed(IDbContext dbContext)
        {
            await SeedClients(dbContext);
            await SeedCircuits(dbContext);
        }

        private static async Task SeedClients(IDbContext dbContext)
        {
            LogHelper.LogMessage("Initializing clients");

            var clientId = "PiHomeMobileClient";
            var client = await dbContext.Clients.Find(f => f.ClientId == clientId).SingleOrDefaultAsync();
            if (client == null)
            {
                var newClient = new ClientEntity
                {
                    Active = true,
                    AllowedOrigin = "*",
                    ClientId = clientId,
                    RefreshTokenLifeTime = 1440
                };

                await dbContext.Clients.InsertOneAsync(newClient);
            }
        }

        private static async Task SeedCircuits(IDbContext dbContext)
        {
            LogHelper.LogMessage("Initializing circuits");

            foreach (var circuitName in Enum.GetNames(typeof(Circuit)))
            {
                var circuit = (Circuit)Enum.Parse(typeof (Circuit), circuitName);
                var circuitState = await dbContext.Circuits.Find(f => f.Circuit == circuit).SingleOrDefaultAsync();

                if (circuitState == null)
                {
                    await dbContext.Circuits.InsertOneAsync(new CircuitStateEntity
                    {
                        Circuit = circuit,
                        Name = "Circuit " + circuitName,
                        State = false
                    });
                }
            }
        }
    }
}
