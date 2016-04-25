using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using piHome.Models.Entities.Auth;

namespace piHome.DataAccess.Interfaces
{
    public interface IAuthDalHelper
    {
        Task<IdentityResult> RegisterUser(UserEntity userEntityModel);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<ClientEntity> FindClient(string clientId);
        Task AddRefreshToken(RefreshTokenEntity tokenEntity);
        Task<RefreshTokenEntity> FindRefreshToken(string refreshTokenId);
        Task RemoveRefreshToken(string refreshTokenId);
    }
}