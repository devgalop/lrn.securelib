using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;

namespace lrn.devgalop.securelib.Tests.Infrastructure.EncryptDecrypt
{
    public class AesCryptServiceTests
    {
        private string _key;
        private string _iv;
        private AesCryptType _aesConfig;
        private AesCryptService _cryptService;
        public AesCryptServiceTests()
        {
            _key = "0IQQftlC4FvJnh35TX1JYw==";
            _iv = "mEu1BD7nPyvWPm0sG3roeQ==";
            _aesConfig = new AesCryptType()
            {
                KeyValue = _key,
                IVValue = _iv
            };
            _cryptService = new AesCryptService(_aesConfig);
        }

        [Fact]
        public void Encrypt_WithEmptyText_ReturnsNoSucceed()
        {
            // Given
            string text = string.Empty;
            AesCryptType aesConfig = _aesConfig;

            // When
            var response = _cryptService.Encrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void Encrypt_WithAesConfigEmpty_ReturnsNoSucceed()
        {
            string text = "This is my test";
            AesCryptType aesConfig = new();

            // When
            var response = _cryptService.Encrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithNoKey_ReturnsNoSucceed()
        {
            string text = "This is my test";
            AesCryptType aesConfig = _aesConfig;
            aesConfig.KeyValue = string.Empty;

            // When
            var response = _cryptService.Encrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithNoIV_ReturnsNoSucceed()
        {
            string text = "This is my test";
            AesCryptType aesConfig = _aesConfig;
            aesConfig.IVValue = string.Empty;
            
            // When
            var response = _cryptService.Encrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithCorrectParameters_ReturnsSucceed()
        {
            string text = "This is my test";
            string textEncrypted = "HwESoNs/wtUF2gKeweOVpw==";
            AesCryptType aesConfig = _aesConfig;
            
            // When
            var response = _cryptService.Encrypt(text,aesConfig);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Text));
            Assert.Equal(response.Text, textEncrypted);
        }

        [Fact]
        public void Encrypt_WithAesParameterNull_ReturnsNoSucceed()
        {
            string text = "This is my test";
            AesCryptType aesConfig = null!;
            
            // When
            var response = _cryptService.Encrypt(text,aesConfig!);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithEmptyText_ReturnsNoSuceed()
        {
             // Given
            string text = string.Empty;
            AesCryptType aesConfig = _aesConfig;

            // When
            var response = _cryptService.Decrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void Decrypt_WithAesConfigEmpty_ReturnsNoSuceed()
        {
            // Given
            string text = "HwESoNs/wtUF2gKeweOVpw==";
            AesCryptType aesConfig = new();

            // When
            var response = _cryptService.Decrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithKeyEmpty_ReturnsNoSuceed()
        {
            // Given
            string text = string.Empty;
            AesCryptType aesConfig = _aesConfig;

            // When
            var response = _cryptService.Decrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithIVEmpty_ReturnsNoSuceed()
        {
            // Given
            string text = "HwESoNs/wtUF2gKeweOVpw==";
            AesCryptType aesConfig = _aesConfig;
            aesConfig.IVValue = string.Empty;

            // When
            var response = _cryptService.Decrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithCorrectParameters_ReturnsSuceed()
        {
            // Given
            string originalText = "This is my test";
            string text = "HwESoNs/wtUF2gKeweOVpw==";
            AesCryptType aesConfig = _aesConfig;
            
            // When
            var response = _cryptService.Decrypt(text,aesConfig);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Text));
            Assert.Equal(response.Text , originalText);
        }

        [Fact]
        public void Decrypt_WithAesParameterNull_ReturnsNoSuceed()
        {
            // Given
            string text = "HwESoNs/wtUF2gKeweOVpw==";
            AesCryptType aesConfig = null!;
            
            // When
            var response = _cryptService.Decrypt(text,aesConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }
    }
}