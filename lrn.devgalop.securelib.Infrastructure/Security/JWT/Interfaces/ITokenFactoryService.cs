using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using Microsoft.IdentityModel.Tokens;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces
{
    public interface ITokenFactoryService
    {
        /// <summary>
        /// Generate JWT Token using customize claims
        /// </summary>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="claims">List of token claims</param>
        /// <param name="duration">Duration of the token in seconds</param>
        /// <returns></returns>
        TokenResponse GenerateToken(string secretKey, List<ClaimRequest> claims, int duration = 60);

        /// <summary>
        /// Validate existing JWT Token
        /// </summary>
        /// <param name="token">Existing token</param>
        /// <param name="tokenValidationParameters">Parameters to be validated</param>
        /// <returns></returns>
        TokenValidationResponse ValidateToken(string token, TokenValidationParameters tokenValidationParameters);

        /// <summary>
        /// Generate a base 64 string with the token generated
        /// </summary>
        /// <param name="durationInMinutes">Duration of token in minutes</param>
        /// <returns></returns>
        TokenResponse GenerateRefreshToken(int durationInMinutes);
    }
}