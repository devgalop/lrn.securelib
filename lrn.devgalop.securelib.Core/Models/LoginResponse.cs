using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Core.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}