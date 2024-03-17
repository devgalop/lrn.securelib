using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Core.Interfaces;
using lrn.devgalop.securelib.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lrn.devgalop.securelib.Core.Extensions
{
    public static class CoreExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}