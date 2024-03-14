using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models;
using Microsoft.Extensions.DependencyInjection;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Extensions
{
    public static class TotpExtensions
    {
        public static void AddTOTP(this IServiceCollection services)
        {
            EpochTicks epochTicks = new();
            services.AddSingleton(_ => epochTicks);
            services.AddAesEncryption();
        }
    }
}