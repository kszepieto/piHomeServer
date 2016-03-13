using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using piHome.DataAccess.Entities;
using piHome.Models.Auth;

namespace piHome.DataAccess
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _database;
        private const string CIRCUITS_STATE = "circuits_state";
        private const string CIRCUITS_STATE_HISTORY = "circuits_state_history";
        private const string SETTINGS = "settings";
        private const string IDENTITY_USERS = "identity_users";
        private const string CLIENTS = "clients";
        private const string REFRESH_TOKENS = "refresh_tokens";
        
        public IMongoCollection<CircuitStateEntity> CircuitsState => _database.GetCollection<CircuitStateEntity>(CIRCUITS_STATE);

        public IMongoCollection<CircuitStateHistory> CircuitsStateHistory => _database.GetCollection<CircuitStateHistory>(CIRCUITS_STATE_HISTORY);

        public IMongoCollection<Setting> Settings => _database.GetCollection<Setting>(SETTINGS);

        public IMongoCollection<IdentityUser> IdentityUsers => _database.GetCollection<IdentityUser>(IDENTITY_USERS);

        public IMongoCollection<Client> Clients => _database.GetCollection<Client>(CLIENTS);

        public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>(REFRESH_TOKENS);

        public DbContext(IMongoDatabase database)
        {
            _database = database;
        }
    }
}
