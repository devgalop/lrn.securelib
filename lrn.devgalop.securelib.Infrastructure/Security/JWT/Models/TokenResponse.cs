using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Models
{
    public class TokenResponse : BaseResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}