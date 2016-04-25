using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using piHome.Models.Entities.Auth;
using piHome.Models.Entities.Circuits;

namespace piHome.DataAccess
{
    public interface IDbContext
    {
        IMongoCollection<CircuitStateEntity> Circuits { get; }
        IMongoCollection<CircuitStateHistoryEntity> CircuitsStateHistory { get; }
        IMongoCollection<IdentityUser> IdentityUsers { get; }
        IMongoCollection<ClientEntity> Clients { get; }
        IMongoCollection<RefreshTokenEntity> RefreshTokens { get; }
    }
}