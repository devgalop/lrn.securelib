using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Services;
using Microsoft.Extensions.DependencyInjection;

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
        }
        
    }
}