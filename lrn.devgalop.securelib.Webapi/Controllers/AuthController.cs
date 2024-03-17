using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost, AllowAnonymous]
        public GenericMethodResponse<LoginResponse> Login([FromBody] LoginRequest request)
        {
            try
            {
                if(request is null) throw new Exception("Invalid request");
                var results = new List<ValidationResult>();
                if(!Validator.TryValidateObject(request, new ValidationContext(request), results, true))
                {
                    var errors = string.Join(",", results.Select(r => {return r.ErrorMessage;}).ToList());
                    throw new Exception($"Invalid request model. errors: {errors}");
                }
                
                var loginResponse = _authService.Login(request);
                if(loginResponse.Status != HttpStatusCode.OK) throw new Exception(loginResponse.ErrorMessage);
                if(loginResponse.Result is null) throw new Exception("Something was wrong in login");

                //Use the method that your prefer to save the token request.
                //If you use a cookie, remember configure it as a HTTPOnly
                Response.Cookies.Append("jwtRefreshToken", loginResponse.Result.RefreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = loginResponse.Result.RefreshTokenExpiration
                });

                return new()
                {
                    Status = HttpStatusCode.OK,
                    Result = new()
                    {
                        Token = loginResponse.Result.Token,
                        Expiration = loginResponse.Result.TokenExpiration
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