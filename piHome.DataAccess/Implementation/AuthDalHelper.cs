using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using piHome.DataAccess.Interfaces;
using piHome.Models.Entities.Auth;

namespace piHome.DataAccess.Implementation
{
    public class AuthDalHelper : BaseDalHelper, IAuthDalHelper
    {
        private readonly UserManager<IdentityUser> _userManager;

        public async Task<IdentityResult> RegisterUser(UserEntity userEntityModel)
        {
            var user = new IdentityUser
            {
                UserName = userEntityModel.UserName
            };

            return await _userManager.CreateAsync(user, userEntityModel.Password);
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password);
        }

        public async Task<ClientEntity> FindClient(string clientId)
        {
            return await _dbContext.Clients.Find(c => c.ClientId == clientId).SingleOrDefaultAsync();
        }

        public async Task AddRefreshToken(RefreshTokenEntity tokenEntity)
        {
            await _dbContext.RefreshTokens.FindOneAndReplaceAsync(r => r.Subject == tokenEntity.Subject && r.ClientId == tokenEntity.ClientId, tokenEntity);
        }

        public async Task<RefreshTokenEntity> FindRefreshToken(string refreshTokenId)
        {
            return await _dbContext.RefreshTokens.Find(r => r.RefreshTokenId == refreshTokenId).SingleOrDefaultAsync();
        }

        public async Task RemoveRefreshToken(string refreshTokenId)
        {
            await _dbContext.RefreshTokens.DeleteOneAsync(t => t.RefreshTokenId == refreshTokenId);
        }

        public AuthDalHelper(IDbContext dbContex)
            :base(dbContex)
        {
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(dbContex.IdentityUsers));
        }
    }
}
