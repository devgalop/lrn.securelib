using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;

namespace lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces
{
    public interface IRsaCryptService : ICryptService<RsaCryptType>
    {
        void GenerateKeys(out string publicKey, out string privateKey);
    }
}