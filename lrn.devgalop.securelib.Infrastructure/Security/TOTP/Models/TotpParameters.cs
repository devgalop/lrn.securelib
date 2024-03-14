using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models
{
    public class TotpParameters
    {
        public TimeSpan ValidForTimeSpan { get; set; }
        public int Size { get; set; } = 4;
        public int ValidFor => ValidForTimeSpan.Seconds;
    }
}