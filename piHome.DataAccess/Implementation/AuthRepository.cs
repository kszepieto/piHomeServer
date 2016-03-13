using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using piHome.DataAccess.Interfaces;
using piHome.Models.Auth;

namespace piHome.DataAccess.Implementation
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public async Task<IdentityResult> RegisterUser(User userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            return await _userManager.CreateAsync(user, userModel.Password);
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password);
        }

        public async Task<Client> FindClient(string clientId)
        {
            return await _dbContext.Clients.Find(c => c.ClientId == clientId).SingleOrDefaultAsync();
        }

        public async Task AddRefreshToken(RefreshToken token)
        {
            await _dbContext.RefreshTokens.FindOneAndReplaceAsync(r => r.Subject == token.Subject && r.ClientId == token.ClientId, token);
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return await _dbContext.RefreshTokens.Find(r => r.RefreshTokenId == refreshTokenId).SingleOrDefaultAsync();
        }

        public async Task RemoveRefreshToken(string refreshTokenId)
        {
            await _dbContext.RefreshTokens.DeleteOneAsync(t => t.RefreshTokenId == refreshTokenId);
        }

        public AuthRepository(IDbContext dbContex)
            :base(dbContex)
        {
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(dbContex.IdentityUsers));
        }
    }
}
