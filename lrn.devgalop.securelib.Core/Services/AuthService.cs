using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Core.Interfaces;
using lrn.devgalop.securelib.Core.Models;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using Microsoft.IdentityModel.Tokens;

namespace lrn.devgalop.securelib.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenFactoryService _tokenFactory;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IAesCryptService _cryptService;
        private readonly AesCryptType _aesConfig;

        public AuthService(
            ITokenFactoryService tokenFactory,
            TokenConfiguration tokenConfiguration,
            IAesCryptService cryptService,
            AesCryptType aesConfig)
        {
            _tokenFactory = tokenFactory;
            _tokenConfiguration = tokenConfiguration;
            _cryptService = cryptService;
            _aesConfig = aesConfig;
        }

        public GenericMethodResponse<Models.TokenResponse> Login(LoginRequest request)
        {
            try
            {
                if (request is null) throw new Exception("Invalid request");
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(request, new ValidationContext(request), results, true))
                {
                    var errors = string.Join(",", results.Select(r => { return r.ErrorMessage; }).ToList());
                    throw new Exception($"Invalid request model. errors: {errors}");
                }

                //Use a database insted of userDemo
                var userDemo = new
                {
                    Username = "devgalop",
                    Password = "iLZ2P1H+fdKABmKiKfPMEg==" //admin1234*
                };
                var passwordEncryptedResponse = _cryptService.Encrypt(request.Password, _aesConfig);
                if (!passwordEncryptedResponse.IsSucceed) throw new Exception("Password field couldn't be encrypted.");
                if (request.UserName != userDemo.Username
                    || passwordEncryptedResponse.Text != userDemo.Password) throw new Exception("Incorrect username or password.");

                //Define your owns claims 
                var claims = new List<ClaimRequest>()
                {
                    new(){ Type = "User", Value = request.UserName},
                    new(){ Type = "Role", Value = "Admin"},
                    new(){ Type = "Gender", Value = "M"}
                };
                var tokenResponse = _tokenFactory.GenerateToken(_tokenConfiguration.SecretKey, claims);
                if (!tokenResponse.IsSucceed) throw new Exception(tokenResponse.ErrorMessage);

                var refreshTokenResponse = _tokenFactory.GenerateRefreshToken(_tokenConfiguration.RefreshTokenTimeExpiration);
                if (!refreshTokenResponse.IsSucceed) throw new Exception(refreshTokenResponse.ErrorMessage);

                return new()
                {
                    Status = HttpStatusCode.OK,
                    Result = new()
                    {
                        Token = tokenResponse.Token,
                        TokenExpiration = tokenResponse.Expiration,
                        RefreshToken = refreshTokenResponse.Token,
                        RefreshTokenExpiration = refreshTokenResponse.Expiration
                    }
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Status = HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }

        public GenericMethodResponse<Models.TokenResponse> RefreshToken(string token, string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) throw new Exception("Invalid user token");
                if (string.IsNullOrEmpty(refreshToken)) throw new Exception("Invalid refresh token");

                var signingKey = new SymmetricSecurityKey(_tokenConfiguration.GetSigingKey(_tokenConfiguration.SecretKey));
                var tokenValidationParams = new TokenValidationParameters()
                {
                    ValidateAudience = _tokenConfiguration.ValidateAudience,
                    ValidateIssuer = _tokenConfiguration.ValidateIssuer,
                    ValidateLifetime = _tokenConfiguration.ValidateLifeTime,
                    ValidateIssuerSigningKey = _tokenConfiguration.ValidateIssuerSigningKey,
                    IssuerSigningKey = signingKey,
                    ClockSkew = TimeSpan.Zero
                };
                var claimsFromTokenResponse = _tokenFactory.GetClaimsFromExpiredToken(token, tokenValidationParams);
                if (!claimsFromTokenResponse.IsSucceed) throw new Exception("Couldn't extract claims from token");

                //Add logic to validate the claims
                var claimsFromToken = claimsFromTokenResponse.Claims;
                var claimUser = claimsFromToken.Where(c => c.Type == "user" && c.Value == "devgalop").FirstOrDefault();
                if (claimUser is null) throw new Exception("Invalid token");

                var claims = claimsFromToken
                .Where(c => c.Type == "user" || c.Type == "Role" || c.Type == "Gender")
                .Select(c => new ClaimRequest
                {
                    Type = c.Type,
                    Value = c.Value
                }).ToList();
                var tokenResponse = _tokenFactory.GenerateToken(_tokenConfiguration.SecretKey, claims);
                if (!tokenResponse.IsSucceed) throw new Exception(tokenResponse.ErrorMessage);

                var refreshTokenResponse = _tokenFactory.GenerateRefreshToken(_tokenConfiguration.RefreshTokenTimeExpiration);
                if (!refreshTokenResponse.IsSucceed) throw new Exception(refreshTokenResponse.ErrorMessage);

                return new()
                {
                    Status = HttpStatusCode.OK,
                    Result = new()
                    {
                        Token = tokenResponse.Token,
                        TokenExpiration = tokenResponse.Expiration,
                        RefreshToken = refreshTokenResponse.Token,
                        RefreshTokenExpiration = refreshTokenResponse.Expiration
                    }
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Status = HttpStatusCode.BadRequest,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}