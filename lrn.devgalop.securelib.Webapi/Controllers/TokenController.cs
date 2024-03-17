using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Core.Interfaces;
using lrn.devgalop.securelib.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lrn.devgalop.securelib.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IAuthService _authService;

        public TokenController(
            ILogger<TokenController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost, AllowAnonymous]
        public GenericMethodResponse<LoginResponse> RefreshToken()
        {
            try
            {
                var token = Request.Headers["Authorization"]
                                .FirstOrDefault()?
                                .Split(" ")
                                .Last();
                if(token is null) throw new Exception("User token is invalid");
                var refreshToken = Request.Cookies
                                        .Where(c => c.Key == "jwtRefreshToken")
                                        .Select(c => c.Value)
                                        .FirstOrDefault();
                if(refreshToken is null) throw new Exception("Refresh token is invalid");
                
                var tokenResponse = _authService.RefreshToken(token, refreshToken);
                if(tokenResponse.Status != HttpStatusCode.OK) throw new Exception(tokenResponse.ErrorMessage);
                if(tokenResponse.Result is null) throw new Exception("Something was wrong with the token generation");
                
                //Use the method that your prefer to save the token request.
                //If you use a cookie, remember configure it as a HTTPOnly
                Response.Cookies.Append("jwtRefreshToken", tokenResponse.Result.RefreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = tokenResponse.Result.RefreshTokenExpiration
                });

                return new()
                {
                    Status = HttpStatusCode.OK,
                    Result = new()
                    {
                        Token = tokenResponse.Result.Token,
                        Expiration = tokenResponse.Result.TokenExpiration
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