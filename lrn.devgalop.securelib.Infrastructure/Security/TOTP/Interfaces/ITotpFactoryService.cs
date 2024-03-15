using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Interfaces
{
    public interface ITotpFactoryService
    {
        TotpResponse Compute();
        TotpResponse ComputeEncrypted();
    }
}