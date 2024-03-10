using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;

namespace lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces
{
    public interface IAesCryptService : ICryptService<AesCryptType>
    {
        byte[] GenerateRandomKey();
    }
}