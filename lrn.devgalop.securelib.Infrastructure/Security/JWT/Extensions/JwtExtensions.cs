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
                .AddCookie(opt => 
                {
                    opt.Cookie.HttpOnly = true;
                    opt.Cookie.Name = "jwtRefreshToken";
                    opt.Cookie.Expiration = TimeSpan.FromHours(2);
                })
                .AddJwtBearer(opt =>
                {
                    var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    //Remeber those conditions when send to validate the token in middleware
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = config.ValidateAudience,
                        ValidateIssuer = config.ValidateIssuer,
                        ValidateLifetime = config.ValidateLifeTime,
                        ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                        IssuerSigningKey = signingKey,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            
        }
        
    }
}