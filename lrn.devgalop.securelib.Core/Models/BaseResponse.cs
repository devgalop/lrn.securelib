using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Core.Models
{
    public class BaseResponse
    {
        public HttpStatusCode Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}