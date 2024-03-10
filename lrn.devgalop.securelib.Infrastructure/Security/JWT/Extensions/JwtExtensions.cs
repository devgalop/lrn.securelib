using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwtSecurity(this IServiceCollection services)
        {
            TokenConfiguration config = new()
            {
                SecretKey = Environment.GetEnvironmentVariable("AUTH_SECRET_KEY") ?? string.Empty
            };
            services.AddTransient(_=> config);
            services.AddTransient<ITokenFactoryService, TokenFactoryService>();

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
                    opt.RequireHttpsMetadata = false;
                    //Remeber those conditions when send to validate the token
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = config.ValidateAudience,
                        ValidateIssuer = config.ValidateIssuer,
                        ValidateLifetime = config.ValidateLifeTime,
                        IssuerSigningKey = signingKey
                    };
                });
            
        }
        
    }
}