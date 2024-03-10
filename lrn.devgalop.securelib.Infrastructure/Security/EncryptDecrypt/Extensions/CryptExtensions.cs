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

        public static void AddRSAEncryption(this IServiceCollection services)
        {
            RsaCryptType rsaConfig = new()
            {
                PublicKey = Environment.GetEnvironmentVariable("RSA_PUBLIC_KEY") ?? string.Empty,
                PrivateKey = Environment.GetEnvironmentVariable("RSA_PRIVATE_KEY") ?? string.Empty,
                FOAEP = false
            };
            services.AddTransient(_ => rsaConfig);
            services.AddTransient<IRsaCryptService,RsaCryptService>();
        }
    }
}