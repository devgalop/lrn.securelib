using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models
{
    public class EpochTicks
    {
        public long UnixEpochTicks {get;} = 621355968000000000L;
        public long TicksToSeconds {get;} = 10000000L;
    }
}