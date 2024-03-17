using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Models
{
    public class TokenConfiguration
    {
        public string SecretKey { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public bool ValidateIssuer { get; set; } = false;
        public string ValidAudience { get; set; } = string.Empty;
        public bool ValidateAudience { get; set; } = false;
        public bool ValidateLifeTime { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = false;
        public int RefreshTokenTimeExpiration { get; set; } = 5;

        public byte[] GetSigingKey(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("Secret key is mandatory");
            return Encoding.UTF8.GetBytes(key);
        }
    }
}