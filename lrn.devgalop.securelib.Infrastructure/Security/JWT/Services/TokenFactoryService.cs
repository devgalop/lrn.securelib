using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using Microsoft.IdentityModel.Tokens;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Services
{
    public class TokenFactoryService : ITokenFactoryService
    {
        public TokenFactoryService()
        {
            
        }

        public TokenResponse GenerateToken(string secretKey, List<ClaimRequest> claims, int duration = 60)
        {
            try
            {
                if(!isValidSecretKey(secretKey)) throw new ArgumentNullException("A secret key is mandatory to generate a token");
                if(claims.Count == 0) throw new ArgumentNullException("A token needs one claim at least");

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(secretKey);
                SymmetricSecurityKey key = new SymmetricSecurityKey(tokenKey);
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(GetClaims(claims)),
                    Expires = DateTime.UtcNow.AddSeconds(duration),
                    SigningCredentials = credentials
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new()
                {
                    IsSucceed = true,
                    Token = tokenHandler.WriteToken(token),
                    Expiration = token.ValidTo.ToLocalTime()
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }

        }
        
        public TokenValidationResponse ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                if(string.IsNullOrEmpty(token)) throw new ArgumentNullException("Token is mandatory to be validated");
                if(tokenValidationParameters is null) throw new ArgumentNullException("Validation parameters cannot be null");

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(
                    token, 
                    tokenValidationParameters, 
                    out SecurityToken validatedToken
                );
                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                return new()
                {
                    IsSucceed = true,
                    JwtToken = jwtToken
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }
        }

        public TokenResponse GenerateRefreshToken(int durationInMinutes)
        {
            try
            {
                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return new()
                {
                    IsSucceed = true,
                    Token = Convert.ToBase64String(randomNumber),
                    Expiration = DateTime.UtcNow.AddMinutes(durationInMinutes)
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }
            
        }

        public ClaimsResponse GetClaimsFromExpiredToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                if(string.IsNullOrEmpty(token)) throw new Exception("Token cannot be null or empty");
                if(tokenValidationParameters is null) throw new Exception("Token validation parameters are required");
                tokenValidationParameters.ValidateLifetime = false;
                var validationResponse = ValidateToken(token, tokenValidationParameters);
                if(!validationResponse.IsSucceed) throw new Exception(validationResponse.ErrorMessage);
                var claims = validationResponse.JwtToken?.Claims;
                if(claims is null || claims.Count() == 0) throw new Exception("There aren't claims to return");
                return new()
                {
                   IsSucceed = true,
                   Claims = claims  
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }
        }

        private bool isValidSecretKey(string key)
        {
            return !string.IsNullOrEmpty(key);
        }

        private Claim[] GetClaims(List<ClaimRequest> claims)
        {
            var claimsResult = claims.Select(c => 
            {
                return new Claim(c.Type, c.Value);
            });
            return claimsResult.ToArray();
        }
    }
}