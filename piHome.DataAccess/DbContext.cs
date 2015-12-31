using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using piHome.DataAccess.Entities;

namespace piHome.DataAccess
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _database;

        public const string CIRCUITS_STATE = "circuits_state";
        public const string CIRCUITS_STATE_HISTORY = "circuits_state_history";
        public const string SETTINGS = "settings";

        public DbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<CircuitStateEntity> CircuitsState => _database.GetCollection<CircuitStateEntity>(CIRCUITS_STATE);

        public IMongoCollection<CircuitStateHistory> CircuitsStateHistory => _database.GetCollection<CircuitStateHistory>(CIRCUITS_STATE_HISTORY);

        public IMongoCollection<Setting> Settings => _database.GetCollection<Setting>(SETTINGS);
    }
}
