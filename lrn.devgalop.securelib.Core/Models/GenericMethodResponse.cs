using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Core.Models
{
    public class GenericMethodResponse<T> : BaseResponse
    {
        public T? Result { get; set; }
    }
}