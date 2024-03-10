using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace lrn.devgalop.securelib.Infrastructure.Security.JWT.Middleware
{
    public class JwtAuthenticationMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenFactoryService _tokenFactoryService;
        private readonly TokenConfiguration _tokenConfiguration;

        public JwtAuthenticationMiddelware(
            RequestDelegate next,
            ITokenFactoryService tokenFactoryService,
            TokenConfiguration tokenConfiguration)
        {
            _next = next;
            _tokenFactoryService = tokenFactoryService;
            _tokenConfiguration = tokenConfiguration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token is not null)
            {
                var validationResponse = _tokenFactoryService.ValidateToken(token, new()
                {
                    //Remember configurations applied in Extensions
                    ValidateAudience = _tokenConfiguration.ValidateAudience,
                    ValidateIssuer = _tokenConfiguration.ValidateIssuer,
                    ValidateLifetime = _tokenConfiguration.ValidateLifeTime,
                    ValidateIssuerSigningKey = _tokenConfiguration.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(_tokenConfiguration.GetSigingKey(_tokenConfiguration.SecretKey)),
                });
                //Add logic after validation 
            }
            await _next(httpContext);
        }

    }
}