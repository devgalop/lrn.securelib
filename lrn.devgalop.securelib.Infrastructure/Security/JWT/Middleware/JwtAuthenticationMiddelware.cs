using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Models;
using Microsoft.AspNetCore.Http;

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
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (token is not null)
            {

            }
            await _next(httpContext);
        }
    }
}