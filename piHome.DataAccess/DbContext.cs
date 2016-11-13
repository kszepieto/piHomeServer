using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using piHome.Models.Entities.Auth;
using piHome.Models.Entities.Circuits;
using piHome.Models.Entities.UserSettings;

namespace piHome.DataAccess
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _database;
        private const string CIRCUITS_STATE = "circuits_state";
        private const string CIRCUITS_STATE_HISTORY = "circuits_state_history";
        private const string IDENTITY_USERS = "identity_users";
        private const string CLIENTS = "clients";
        private const string REFRESH_TOKENS = "refresh_tokens";
        private const string CIRCUITS_HANDLING_SETS = "circuits_handling_sets";
        
        public IMongoCollection<CircuitStateEntity> Circuits => _database.GetCollection<CircuitStateEntity>(CIRCUITS_STATE);

        public IMongoCollection<CircuitStateHistoryEntity> CircuitsStateHistory => _database.GetCollection<CircuitStateHistoryEntity>(CIRCUITS_STATE_HISTORY);

        public IMongoCollection<IdentityUser> IdentityUsers => _database.GetCollection<IdentityUser>(IDENTITY_USERS);

        public IMongoCollection<ClientEntity> Clients => _database.GetCollection<ClientEntity>(CLIENTS);

        public IMongoCollection<RefreshTokenEntity> RefreshTokens => _database.GetCollection<RefreshTokenEntity>(REFRESH_TOKENS);

        public IMongoCollection<CircuitsHandlingSetEntity> CircuitsHandlingSets => _database.GetCollection<CircuitsHandlingSetEntity>(CIRCUITS_HANDLING_SETS);

        public DbContext(IMongoDatabase database)
        {
            _database = database;
        }
    }
}
