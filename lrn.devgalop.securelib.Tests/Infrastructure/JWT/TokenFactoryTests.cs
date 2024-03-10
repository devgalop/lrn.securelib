using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Tests.Infrastructure.JWT
{
    public class TokenFactoryTests
    {
        private readonly TokenFactoryService _service;
        public TokenFactoryTests()
        {
            _service = new TokenFactoryService();
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
            string sampleKey = "558ad86aa09727842f6f81a0a84d829b9cc5e7965c0d84415fa5510c306d7cd0";
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
    }
}