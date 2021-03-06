using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using piHome.DataAccess.Interfaces;
using piHome.Models.Entities.Auth;

namespace piHome.WebHost.AuthProviders
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IAuthDalHelper _authDalHelper;

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshTokenEntity()
            {
                RefreshTokenId = AuthHelper.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            await _authDalHelper.AddRefreshToken(token);
            context.SetToken(refreshTokenId);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            context.OwinContext.Response.Headers.Remove("Access-Control-Allow-Origin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var hashedTokenId = AuthHelper.GetHash(context.Token);

            var refreshToken = await _authDalHelper.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await _authDalHelper.RemoveRefreshToken(hashedTokenId);
            }
        }

        public SimpleRefreshTokenProvider(IAuthDalHelper authDalHelper)
        {
            _authDalHelper = authDalHelper;
        }
    }
}