using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;

namespace lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Services
{
    public class RsaCryptService : IRsaCryptService
    {
        private readonly RsaCryptType _cryptType;

        public RsaCryptService(RsaCryptType cryptType)
        {
            _cryptType = cryptType;
        }

        public CryptResponse Decrypt(string textEncrypted, RsaCryptType cryptParams)
        {
            try
            {
                if(string.IsNullOrEmpty(textEncrypted))throw new ArgumentNullException("Text to decrypt cannot be null");
                if(cryptParams is null) throw new ArgumentNullException("RSA validation parameters cannot be null");
                if(string.IsNullOrEmpty(cryptParams.PrivateKey)) throw new ArgumentNullException("Private key is mandatory");
                byte[] decryptedData;

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(cryptParams.PrivateParameters);

                    decryptedData = rsa.Decrypt(Convert.FromBase64String(textEncrypted), cryptParams.FOAEP);
                }
                return new()
                {
                    IsSucceed = true,
                    Text = Encoding.UTF8.GetString(decryptedData)
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }
        }

        public CryptResponse Encrypt(string inputText, RsaCryptType cryptParams)
        {
            try
            {
                if(string.IsNullOrEmpty(inputText))throw new ArgumentNullException("Text to encrypt cannot be null");
                if(cryptParams is null) throw new ArgumentNullException("RSA validation parameters cannot be null");
                if(string.IsNullOrEmpty(cryptParams.PublicKey)) throw new ArgumentNullException("Public key is mandatory");

                byte[] encryptedData;

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.ImportParameters(cryptParams.PublicParameters);
                    encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(inputText), cryptParams.FOAEP);
                }
                return new()
                {
                    IsSucceed = true,
                    Text = Convert.ToBase64String(encryptedData)
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }
        }

        public void GenerateKeys(out string publicKey, out string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = RSAParametersToString(rsa.ExportParameters(false));
                privateKey = RSAParametersToString(rsa.ExportParameters(true));
            }
        }

        private string RSAParametersToString(RSAParameters rsaParams)
        {
            StringBuilder sb = new StringBuilder();
            if (rsaParams.Modulus is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.Modulus));
            if (rsaParams.Exponent is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.Exponent));
            if (rsaParams.P is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.P));
            if (rsaParams.Q is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.Q));
            if (rsaParams.DP is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.DP));
            if (rsaParams.DQ is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.DQ));
            if (rsaParams.InverseQ is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.InverseQ));
            if (rsaParams.D is not null)
                sb.AppendLine(Convert.ToBase64String(rsaParams.D));
            return sb.ToString();
        }
    }
}