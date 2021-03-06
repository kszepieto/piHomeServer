﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using piHome.DataAccess.Interfaces;

namespace piHome.WebHost.AuthProviders
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthDalHelper _authDalHelper;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //validate if calling application ("client") sent valid clientId

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            context.TryGetFormCredentials(out clientId, out clientSecret);

            if (string.IsNullOrEmpty(clientId))
            {
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return;
            }

            var client = await _authDalHelper.FindClient(clientId);
            if (client == null)
            {
                context.SetError("invalid_clientId", $"Client '{clientId}' is not registered in the system.");
                return;
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return;
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null)
            {
                allowedOrigin = "*";
            }

            context.OwinContext.Response.Headers.Remove("Access-Control-Allow-Origin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            
            var user = await _authDalHelper.FindUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "as:client_id", context.ClientId ?? string.Empty},
                {"userName", context.UserName}
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public SimpleAuthorizationServerProvider(IAuthDalHelper authDalHelper)
        {
            _authDalHelper = authDalHelper;
        }
    }
}
