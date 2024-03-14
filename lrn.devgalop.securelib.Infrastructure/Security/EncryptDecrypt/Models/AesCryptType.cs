using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;

namespace lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models
{
    public class AesCryptType : ICryptType
    {
        public string KeyValue { get; set; } = string.Empty;
        public string IVValue { get; set; } = string.Empty;
        public byte[] Key => GenerateRandomKey(KeyValue);
        public byte[] IV => GenerateRandomKey(IVValue);

        public byte[] GenerateRandomKey(string value)
        {
            byte[] customBytes = Encoding.UTF8.GetBytes(value);
            byte[] key = new byte[16]; // 128 bits
            Array.Copy(customBytes, key, Math.Min(customBytes.Length, key.Length));

            // If custom bytes are shorter than 16 bytes, pad with zeros
            for (int i = customBytes.Length; i < key.Length; i++)
            {
                key[i] = 0;
            }
            return key;
        }
    }
}