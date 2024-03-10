using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Extensions
{
    public static class CryptExtensions
    {
        public static void AddAesEncryption(this IServiceCollection services)
        {
            AesCryptType aesConfig = new()
            {
                KeyValue = Environment.GetEnvironmentVariable("AES_KEY")?? string.Empty,
                IVValue = Environment.GetEnvironmentVariable("AES_IV")?? string.Empty
            };
            services.AddTransient(_ => aesConfig);
            services.AddTransient<IAesCryptService, AesCryptService>();
        }
    }
}