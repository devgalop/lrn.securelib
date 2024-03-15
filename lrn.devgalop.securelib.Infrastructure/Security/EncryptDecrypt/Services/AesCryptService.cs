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
    public class AesCryptService : IAesCryptService
    {
        private readonly AesCryptType _aesConfig;

        public AesCryptService(AesCryptType aesConfig)
        {
            _aesConfig = aesConfig;
        }

        public CryptResponse Decrypt(string textEncrypted, AesCryptType cryptParams)
        {
            try
            {
                if (string.IsNullOrEmpty(textEncrypted)) throw new ArgumentNullException("Text to decrypt is empty");
                if (cryptParams is null) throw new ArgumentNullException("Encryption configurations cannot be null");
                if(string.IsNullOrEmpty(cryptParams.KeyValue)) throw new ArgumentNullException("Secret key value cannot be null");
                if(string.IsNullOrEmpty(cryptParams.IVValue)) throw new ArgumentNullException("IV value cannot be null");
                string decrypted;
                byte[] cipherText = Convert.FromBase64String(textEncrypted);

                // Create an Aes object with the specified key and IV.
                using (Aes aes = Aes.Create())
                {
                    aes.Key = cryptParams.Key ?? throw new ArgumentNullException("Secret key value cannot be null");
                    aes.IV = cryptParams.IV ?? throw new ArgumentNullException("IV value cannot be null");

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    // Create a new MemoryStream object to contain the decrypted bytes.
                    using (MemoryStream memoryStream = new MemoryStream(cipherText))
                    {
                        // Create a CryptoStream object to perform the decryption.
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            // Decrypt the ciphertext.
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                decrypted = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
                return new()
                {
                    IsSucceed = true,
                    Text = decrypted
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

        public CryptResponse Encrypt(string inputText, AesCryptType cryptParams)
        {
            try
            {
                if (string.IsNullOrEmpty(inputText)) throw new ArgumentNullException("Text to encrypt is empty");
                if (cryptParams is null) throw new ArgumentNullException("Encryption configurations cannot be null");
                if(string.IsNullOrEmpty(cryptParams.KeyValue)) throw new ArgumentNullException("Secret key value cannot be null");
                if(string.IsNullOrEmpty(cryptParams.IVValue)) throw new ArgumentNullException("IV value cannot be null");
                byte[] encrypted;
                using (Aes aes = Aes.Create())
                {
                    aes.Key = cryptParams.Key ?? throw new ArgumentNullException("Secret key value cannot be null");
                    aes.IV = cryptParams.IV ?? throw new ArgumentNullException("IV value cannot be null");

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    // Create a new MemoryStream object to contain the encrypted bytes.
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Create a CryptoStream object to perform the encryption.
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            // Encrypt the plaintext.
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(inputText);
                            }

                            encrypted = memoryStream.ToArray();
                        }
                    }
                }

                return new()
                {
                    IsSucceed = true,
                    Text = Convert.ToBase64String(encrypted)
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

        public string GenerateKey()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedText = sha256.ComputeHash(GenerateRandomKey());
                return BitConverter.ToString(hashedText).Replace("-", "");
            }
        }

        private byte[] GenerateRandomKey()
        {
            byte[] key = new byte[16]; // 128 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }


    }
}