using MongoDB.Driver;
using piHome.DataAccess.Entities;

namespace piHome.DataAccess
{
    public interface IDbContext
    {
        IMongoCollection<CircuitStateEntity> CircuitsState { get; }
        IMongoCollection<CircuitStateHistory> CircuitsStateHistory { get; }
        IMongoCollection<Setting> Settings { get; }
    }
}