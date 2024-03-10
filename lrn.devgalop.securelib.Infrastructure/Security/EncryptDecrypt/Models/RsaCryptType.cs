using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;

namespace lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models
{
    public class RsaCryptType : ICryptType
    {
        public string PublicKey { get; set; } = string.Empty;
        public RSAParameters PublicParameters => GetRSAParameters(PublicKey);
        public string PrivateKey { get; set; } = string.Empty;
        public RSAParameters PrivateParameters => GetRSAParameters(PrivateKey);
        public bool FOAEP { get; set; }
        private RSAParameters GetRSAParameters(string key)
        {
            RSAParameters parameters = new();
            try
            {
                string[] lines = key.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                parameters.Modulus = Convert.FromBase64String(lines[0]);
                if (lines.Length > 1)
                    parameters.Exponent = Convert.FromBase64String(lines[1]);
                if (lines.Length > 2)
                    parameters.P = Convert.FromBase64String(lines[2]);

                if (lines.Length > 3)
                    parameters.Q = Convert.FromBase64String(lines[3]);

                if (lines.Length > 4)
                    parameters.DP = Convert.FromBase64String(lines[4]);

                if (lines.Length > 5)
                    parameters.DQ = Convert.FromBase64String(lines[5]);

                if (lines.Length > 6)
                    parameters.InverseQ = Convert.FromBase64String(lines[6]);

                if (lines.Length > 7)
                    parameters.D = Convert.FromBase64String(lines[7]);
                return parameters;
            }
            catch (Exception)
            {
                return new();
            }
            
        }
    }
}