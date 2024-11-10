using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public class JwtTokenHandler : JwtBearerEvents
    {
        private const string BearerPrefix = "Bearer ";
        private readonly string headerName;
        private readonly string audience;
        public JwtTokenHandler(string headerName, string audience)
        {
            this.OnMessageReceived = this.MessageReceivedHandler;
            this.headerName = headerName;
            this.OnTokenValidated = this.TokenValidatedHandler;
            this.audience = audience;
        }

        // if header not found, will default to authorization
        private Task MessageReceivedHandler(MessageReceivedContext context)
        {
            if (context.Request.Headers.TryGetValue(this.headerName, out var headerValue))
            {
                string token = headerValue;
                if (!string.IsNullOrEmpty(token) && token.StartsWith(BearerPrefix))
                {
                    token = token[BearerPrefix.Length..];
                }

                context.Token = token;
            }

            return Task.CompletedTask;
        }

        private Task TokenValidatedHandler(TokenValidatedContext context)
        {
            if (context.SecurityToken is not JwtSecurityToken jwt)
            {
                context.Fail("jwt not parsed!");
                return Task.CompletedTask;
            }

            if (jwt.Audiences.Contains(this.audience))
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }