using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models
{
    public class TotpResponse : BaseResponse
    {
        public string TotpCode { get; set; } = string.Empty;
    }
}