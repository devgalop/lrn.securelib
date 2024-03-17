using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace lrn.devgalop.securelib.Tests.Infrastructure.JWT
{
    public class TokenFactoryTests
    {
        private string _secretKey;
        private readonly TokenFactoryService _service;
        public TokenFactoryTests()
        {
            _service = new TokenFactoryService();
            _secretKey = "558ad86aa09727842f6f81a0a84d829b9cc5e7965c0d84415fa5510c306d7cd0";
        }

        [Fact]
        public void GenerateToken_WithEmptyKey_ReturnsNotSucceed()
        {
            // Given
            string sampleKey = string.Empty;
            List<ClaimRequest> claimsSamples = new()
            {
                new ClaimRequest()
                {
                    Value = "SampleName",
                    Type = "Name"
                },
                new ClaimRequest()
                {
                    Value = "SampleRole",
                    Type = "Role"
                }
            };
            
            // When
            var response = _service.GenerateToken(sampleKey, claimsSamples);
            
            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GenerateToken_WithEmptyClaims_ReturnsNotSucceed()
        {
            // Given
            string sampleKey = "Th1$_15_my_$3cre7_k3Y";
            List<ClaimRequest> claimsSamples = new();

            //When
            var response = _service.GenerateToken(sampleKey, claimsSamples);
            
            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GenerateToken_WithInvalidSecretKey_ReturnsNotSucceed()
        {
            string sampleKey = "Th1$_15_my_$3cre7_k3Y";
            List<ClaimRequest> claimsSamples = new()
            {
                new ClaimRequest()
                {
                    Value = "SampleName",
                    Type = "Name"
                },
                new ClaimRequest()
                {
                    Value = "SampleRole",
                    Type = "Role"
                }
            };
            
            // When
            var response = _service.GenerateToken(sampleKey, claimsSamples);
            
            // Then
            Assert.False(response.IsSucceed);        
        }

        [Fact]
        public void GenerateToken_WithCorrectSecretKey_ReturnsSucceed()
        {
            string sampleKey = _secretKey;
            List<ClaimRequest> claimsSamples = new()
            {
                new ClaimRequest()
                {
                    Value = "SampleName",
                    Type = "Name"
                },
                new ClaimRequest()
                {
                    Value = "SampleRole",
                    Type = "Role"
                }
            };
            
            // When
            var response = _service.GenerateToken(sampleKey, claimsSamples);
            
            // Then
            Assert.True(response.IsSucceed);   
            Assert.True(!string.IsNullOrEmpty(response.Token));
        }

        [Fact]
        public void ValidateToken_WithTokenEmpty_ReturnsNoSucceed()
        {
            // Given
            string token = string.Empty;
            var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateAudience = config.ValidateAudience,
                ValidateIssuer = config.ValidateIssuer,
                ValidateLifetime = config.ValidateLifeTime,
                ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                IssuerSigningKey = signingKey
            };

            // When
            var response = _service.ValidateToken(token,parameters);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void ValidateToken_WithTokenInvalid_ReturnsNoSucceed()
        {
            // Given
            string token = "";
            var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateAudience = config.ValidateAudience,
                ValidateIssuer = config.ValidateIssuer,
                ValidateLifetime = config.ValidateLifeTime,
                ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                IssuerSigningKey = signingKey
            };

            // When
            var response = _service.ValidateToken(token,parameters);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void ValidateToken_WithTokenParametersNull_ReturnsNoSucceed()
        {
            // Given
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.6NASL3bMHcx4QMLjQXBGugiP0lHFifhr56DsM1bITkI";
            var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            TokenValidationParameters? parameters = null;

            // When
            var response = _service.ValidateToken(token,parameters!);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void ValidateToken_WithCorrectInput_ReturnsSucceed()
        {
            // Given
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.6NASL3bMHcx4QMLjQXBGugiP0lHFifhr56DsM1bITkI";
            var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateAudience = config.ValidateAudience,
                ValidateIssuer = config.ValidateIssuer,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                IssuerSigningKey = signingKey
            };

            // When
            var response = _service.ValidateToken(token,parameters);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(response.JwtToken is not null);
        }

        [Fact]
        public void GenerateRefreshToken_WithNegativeDuration_ReturnsNoSuceed()
        {
            // Given
            int duration = -1;

            // When
            var response = _service.GenerateRefreshToken(duration);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GenerateRefreshToken_WithZeroDuration_ReturnsNoSuceed()
        {
            // Given
            int duration = 0;

            // When
            var response = _service.GenerateRefreshToken(duration);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GenerateRefreshToken_WithCorrectDuration_ReturnsSuceed()
        {
            // Given
            int duration = 2;

            // When
            var response = _service.GenerateRefreshToken(duration);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Token));
        }

        [Fact]
        public void GetClaimsFromExpiredToken_WithEmptyToken_ReturnsNoSuceed()
        {
            // Given
            string token = string.Empty;
             var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateAudience = config.ValidateAudience,
                ValidateIssuer = config.ValidateIssuer,
                ValidateLifetime = config.ValidateLifeTime,
                ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                IssuerSigningKey = signingKey
            };

            // When
            var response = _service.GetClaimsFromExpiredToken(token, parameters);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GetClaimsFromExpiredToken_WithNullToken_ReturnsNoSuceed()
        {
            // Given
            string token = null!;
             var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateAudience = config.ValidateAudience,
                ValidateIssuer = config.ValidateIssuer,
                ValidateLifetime = config.ValidateLifeTime,
                ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                IssuerSigningKey = signingKey
            };

            // When
            var response = _service.GetClaimsFromExpiredToken(token, parameters);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GetClaimsFromExpiredToken_WithNullParameters_ReturnsNoSuceed()
        {
            // Given
            string token = string.Empty;
             var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            TokenValidationParameters parameters = null!;

            // When
            var response = _service.GetClaimsFromExpiredToken(token, parameters);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void GetClaimsFromExpiredToken_WithCorrectParameters_ReturnsSuceed()
        {
            // Given
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.6NASL3bMHcx4QMLjQXBGugiP0lHFifhr56DsM1bITkI";
             var config = new TokenConfiguration()
            {
                SecretKey = _secretKey
            };
            var signingKey = new SymmetricSecurityKey(config.GetSigingKey(config.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateAudience = config.ValidateAudience,
                ValidateIssuer = config.ValidateIssuer,
                ValidateLifetime = config.ValidateLifeTime,
                ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                IssuerSigningKey = signingKey
            };

            // When
            var response = _service.GetClaimsFromExpiredToken(token, parameters);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(response.Claims.Any());        
        }
    }
}