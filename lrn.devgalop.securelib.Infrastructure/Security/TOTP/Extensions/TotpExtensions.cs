using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Extensions
{
    public static class TotpExtensions
    {
        public static void AddTOTP(this IServiceCollection services)
        {
            EpochTicks epochTicks = new();
            TotpParameters totpParameters = new()
            {
                Size = 6,
                ValidForTimeSpan = TimeSpan.FromSeconds(30)  
            };
            services.AddSingleton(_ => epochTicks);
            services.AddTransient(_ => totpParameters);
            services.AddAesEncryption();
            services.AddTransient<ITotpFactoryService,TotpFactoryService>();
        }
    }
}