using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using piHome.DataAccess.Entities;
using piHome.Models.Auth;

namespace piHome.DataAccess
{
    public interface IDbContext
    {
        IMongoCollection<CircuitStateEntity> CircuitsState { get; }
        IMongoCollection<CircuitStateHistory> CircuitsStateHistory { get; }
        IMongoCollection<Setting> Settings { get; }
        IMongoCollection<IdentityUser> IdentityUsers { get; }
        IMongoCollection<Client> Clients { get; }
        IMongoCollection<RefreshToken> RefreshTokens { get; }
    }
}