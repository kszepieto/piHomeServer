using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using piHome.Models.Auth;

namespace piHome.DataAccess.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(User userModel);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<Client> FindClient(string clientId);
        Task AddRefreshToken(RefreshToken token);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        Task RemoveRefreshToken(string refreshTokenId);
    }
}