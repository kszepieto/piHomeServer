using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Conventions;
using piHome.Models.Auth;
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
            var clientIdField = new StringFieldDefinition<Client>(ExpressionsHelper.GetPropertyName<Client>(c => c.ClientId));
            var clientIdIndex = new IndexKeysDefinitionBuilder<Client>().Ascending(clientIdField);
            await dbContext.Clients.Indexes.CreateOneAsync(clientIdIndex, new CreateIndexOptions() { Unique = true });

            var refreshTokenIdField = new StringFieldDefinition<RefreshToken>(ExpressionsHelper.GetPropertyName<RefreshToken>(c => c.RefreshTokenId));
            var refreshTokenIdIndex = new IndexKeysDefinitionBuilder<RefreshToken>().Ascending(refreshTokenIdField);
            await dbContext.RefreshTokens.Indexes.CreateOneAsync(refreshTokenIdIndex, new CreateIndexOptions() { Unique = true });
        }

        private static async Task Seed(IDbContext dbContext)
        {
            var clientId = "PiHomeMobileClient";
            var client = await dbContext.Clients.Find(f => f.ClientId == clientId).SingleOrDefaultAsync();
            if (client == null)
            {
                var newClient = new Client
                {
                    Active = true,
                    AllowedOrigin = "*",
                    ClientId = clientId,
                    RefreshTokenLifeTime = 1440
                };

                await dbContext.Clients.InsertOneAsync(newClient);
            }
        }
    }
}
