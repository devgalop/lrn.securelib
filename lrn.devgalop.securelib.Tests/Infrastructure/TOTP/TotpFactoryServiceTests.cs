using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Services;

namespace lrn.devgalop.securelib.Tests.Infrastructure.TOTP
{
    public class TotpFactoryServiceTests
    {
        private EpochTicks _epochTicks;
        private TotpParameters _totpParams;
        private AesCryptType _aesConfig;
        private IAesCryptService _cryptService;

        public TotpFactoryServiceTests()
        {
            _epochTicks = new();
            _totpParams = new()
            {
                Size = 4,
                ValidForTimeSpan = TimeSpan.FromSeconds(30)
            };
            _aesConfig = new()
            {
                KeyValue = "0IQQftlC4FvJnh35TX1JYw==",
                IVValue = "mEu1BD7nPyvWPm0sG3roeQ=="
            };
            _cryptService = new AesCryptService(_aesConfig);    
        }

        [Fact]
        public void Compute_WithEpochTicksNull_ReturnNoSuceed()
        {
            // Given
            EpochTicks epochTicks = null!;
            TotpFactoryService totpFactoryService = new(epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithTotpParametersNull_ReturnNoSuceed()
        {
            // Given
            TotpParameters totpParameters = null!;
            TotpFactoryService totpFactoryService = new(_epochTicks, totpParameters, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithAesConfigurationNull_ReturnNoSuceed()
        {
            // Given
            AesCryptType aesConfig = null!;
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithTotpParamSizeWithZero_ReturnNoSuceed()
        {
            // Given
            _totpParams.Size = 0;
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithTotpParamSizeNegative_ReturnNoSuceed()
        {
            // Given
            _totpParams.Size = -1;
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithTotpParamValidForNegative_ReturnNoSuceed()
        {
            // Given
            _totpParams.ValidForTimeSpan = TimeSpan.FromSeconds(-1);
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithAesKeyEmpty_ReturnNoSuceed()
        {
            // Given
            _aesConfig.KeyValue = string.Empty;
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithAesCryptServiceNull_ReturnSuceed()
        {
            // Given
            AesCryptService cryptService = null!;
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void Compute_WithCorrectParameters_ReturnSuceed()
        {
            // Given
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.Compute();

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void ComputeEncrypted_WithAesCryptServiceNull_ReturnNoSuceed()
        {
            // Given
            AesCryptService cryptService = null!;
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, cryptService);
            
            // When
            var response = totpFactoryService.ComputeEncrypted();

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.TotpCode));
        }

        [Fact]
        public void ComputeEncrypted_WithCorrectParameters_ReturnSuceed()
        {
            // Given
            TotpFactoryService totpFactoryService = new(_epochTicks, _totpParams, _aesConfig, _cryptService);
            
            // When
            var response = totpFactoryService.ComputeEncrypted();

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.TotpCode));
        }
    }
}