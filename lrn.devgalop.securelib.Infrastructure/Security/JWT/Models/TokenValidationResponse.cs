using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Models
{
    public class TokenValidationResponse : BaseResponse
    {
        public JwtSecurityToken? JwtToken {get; set;}
    }
}