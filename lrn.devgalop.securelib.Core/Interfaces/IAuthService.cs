using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Core.Models;

namespace lrn.devgalop.securelib.Core.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Validate request to login and generate tokens
        /// </summary>
        /// <param name="request">Request model to login</param>
        /// <returns></returns>
        GenericMethodResponse<Models.TokenResponse> Login(LoginRequest request);

        /// <summary>
        /// Refresh JWT Tokens
        /// </summary>
        /// <param name="token">User bearer token</param>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns></returns>
        GenericMethodResponse<Models.TokenResponse> RefreshToken(string token, string refreshToken);
    }
}