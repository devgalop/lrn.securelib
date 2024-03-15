using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;

namespace lrn.devgalop.securelib.Tests.Infrastructure.EncryptDecrypt
{
    public class RsaCryptServiceTests
    {
        private RsaCryptType _rsaConfig;
        private RsaCryptService _cryptService;
        public RsaCryptServiceTests()
        {
            _rsaConfig = new()
            {
                FOAEP = false,
                PublicKey = "xHxQtZY8VGHA3hA/MXG0+J5ROov8Zd6m2Yi3MHoph9M4d44yw8ciPwpmZpyW+vlBJw9DfD4mlcA5JEX+SdHZManO+l7plYYJK1M3YlEVR69kwkTWTMnDKdqSupRnBumz8P+pjfaEsS+WfFWUGbh37kJ1Umz6cJ3ohDM3DeuNATRF89a2F7TubBzhjFz7HXDl/NPQqY+/RZ17L875qpzrYb/WvXYggMZrH94FgGUhiob0FwQ7kGJxqXDU6cHc1M8HGrEhkzkvFdJXOFbdfywtR7zSP44ZQW6YdeGZDYdynqpAGeErqgzjU7o68z/O+H9/x26VzwmYCZ89W+yoPfzb+Q==\r\nAQAB\r\n",
                PrivateKey = "xHxQtZY8VGHA3hA/MXG0+J5ROov8Zd6m2Yi3MHoph9M4d44yw8ciPwpmZpyW+vlBJw9DfD4mlcA5JEX+SdHZManO+l7plYYJK1M3YlEVR69kwkTWTMnDKdqSupRnBumz8P+pjfaEsS+WfFWUGbh37kJ1Umz6cJ3ohDM3DeuNATRF89a2F7TubBzhjFz7HXDl/NPQqY+/RZ17L875qpzrYb/WvXYggMZrH94FgGUhiob0FwQ7kGJxqXDU6cHc1M8HGrEhkzkvFdJXOFbdfywtR7zSP44ZQW6YdeGZDYdynqpAGeErqgzjU7o68z/O+H9/x26VzwmYCZ89W+yoPfzb+Q==\r\nAQAB\r\n0irhl+wPVX5eZrtInPaHm0WbKvWf6RZSZ7j/fIlMJKVavWVdPeH1K5bm8xc5PRpc7CcDbol1aOgW7QX4cMVaDhPyl+/qkMAW/bflpt5QfoFoJPvM8hDYIZ8BStpYvB0cJNny9+62ngZC3T5AUgeipWhoO+Xd0sS00k9MOkIWkA8=\r\n71WbveGKJIk7Qls3I6Qowi4mNzyFDyrTOjbOCiDZiFQpE0QoUjMoweRnSPhgOKZi7fsDMZwRhS0OSlXTqXU6m9UFIo1b0+7+anVB+optRbKfH6tJAqZ7f45tcNgc85G8Fjmcr40WFGOAfAwI2RVQvKgZtof9QjtXy6ZjyoZ8y3c=\r\ne+PtUmpEa8hvi62xU138eGqi2sMqffO7pgsnCFnDOgMDp4weWAFgTk7JW9l/02Y8CrXAzyNWw/20rgqL9qZPvObDZg92vdeOdgg255Q77ScJpmuM19FYJxGdaGuoTVP3qD2WfdMJccQNHHC65+A6X7lZYJ3TkpCEa0l1jQ4yUL8=\r\nWURY/o2s9MuqqqlS+5wzB5om7ttH1cxnVIM0flqaSFr7pw/46g/i187VJ0ZcvURyCclh+5L2hfG0Ls2sULxEy/K8I5MN+RJfGl356fTPpPtmlm1Qoghub7kz4K76vW5R9QgwBNpereQe/CWQ90cYAgXA8W/valCwAmlhNxzKVyk=\r\nZh7zbOomdZNhNKCI106W3uzqq+2MpK/fJXG6nJ2MeA0FqAk38fbAidfE87y/YHbtbc7qFyx3r1rZcRxHwFdAeyinJATMNsDhWBL7HvuXG+yN7MJvdxa+1+C6Hex2vSd3rE6NtDlr7PK+nLm0shDD8nlQbIr1T+EOgbWkCHI6JTI=\r\ngUWBHFeBzcqax/9MdE1pUYFn0+sx3hwFDm4iiGR3WoOfTRljUOd9IpjHEo/HzcucEAS808tYO4JClhju2saT7VbZdGRprhSHh9eyQp8yLv24fxAnJlJAcxL5IoRs3GF6H/81IM+mMaUrjxYG4H07eqiC4/4LL3jU99VyTVhCZx1Dg1i0P02c4FxtEy7ARjJCIlzcDPlVgx0O9Et/zDin+JA1N304p3wtXc20LN/zYEf8TfFlBYLzKOgu+q1MoYfwtLxUlF4uyDVZ2LP1BpUR0QPL6xpwf+SJ+UKJXFdtN7en0qZjHsLg662gfjf3SoT/L9C1FUUytWOgN/WBXchR2Q==\r\n"
            };
            _cryptService = new(_rsaConfig);
        }

        [Fact]
        public void Encrypt_WithTextEmpty_ReturnsNoSucceed()
        {
            // Given
            string text = string.Empty;
            
            // When
            var response = _cryptService.Encrypt(text,_rsaConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithRsaParametersNull_ReturnsNoSucceed()
        {
            // Given
            _rsaConfig = null!;
            string text = "This is my test";
            
            // When
            var response = _cryptService.Encrypt(text,_rsaConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithPublicKeyEmpty_ReturnsNoSucceed()
        {
            // Given
            string text = "This is my test";
            _rsaConfig.PublicKey = string.Empty;
            // When
            var response = _cryptService.Encrypt(text,_rsaConfig);

            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithPrivateKeyEmpty_ReturnsSucceed()
        {
            // Given
            string text = "This is my test";
            _rsaConfig.PrivateKey = string.Empty;
            // When
            var response = _cryptService.Encrypt(text,_rsaConfig);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Encrypt_WithCorrectParameters_ReturnsSucceed()
        {
            // Given
            string text = "This is my test";
            // When
            var response = _cryptService.Encrypt(text,_rsaConfig);

            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithTextEmpty_ReturnNoSucceed()
        {
            // Given
            string textEncrypted = string.Empty;
            
            // When
            var response = _cryptService.Decrypt(textEncrypted, _rsaConfig);
            
            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithRsaParametersNull_ReturnNoSucceed()
        {
            // Given
            string textEncrypted = "VK5284DVyRGKCSmkHVq9jWiWti9TcWuzUu6vLOD+uXHURuuKzsMKSkA5T5i7v+9MD0PRxtBqMw7VZb4eeVCKfJKpi7PSlPC8WasrifcBta2cG4949kF0HDEVK5i5c2Y1Us0XmjitSUltRERZwrpmoBOvIOxeDtK5ivF7oSQFbellS1pBBB4Ysqr6ZUsLhcuNctRqezSmZHu3U82XVZShVt3bEQynRI8+lLeGW/kesSJPaXsEuWyf8ShG+Xuvvf1ynLvcnjI2c8Prq0SF8WBFcqUwVIvT6LVzz6OYipG2gkegMInYnqu/ozqL5LfV/MgMvRxQ91ogCQ3afwhak5myRw==";
            _rsaConfig = null!;
            // When
            var response = _cryptService.Decrypt(textEncrypted, _rsaConfig);
            
            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithPrivateKeyEmpty_ReturnNoSucceed()
        {
            // Given
            string textEncrypted = "VK5284DVyRGKCSmkHVq9jWiWti9TcWuzUu6vLOD+uXHURuuKzsMKSkA5T5i7v+9MD0PRxtBqMw7VZb4eeVCKfJKpi7PSlPC8WasrifcBta2cG4949kF0HDEVK5i5c2Y1Us0XmjitSUltRERZwrpmoBOvIOxeDtK5ivF7oSQFbellS1pBBB4Ysqr6ZUsLhcuNctRqezSmZHu3U82XVZShVt3bEQynRI8+lLeGW/kesSJPaXsEuWyf8ShG+Xuvvf1ynLvcnjI2c8Prq0SF8WBFcqUwVIvT6LVzz6OYipG2gkegMInYnqu/ozqL5LfV/MgMvRxQ91ogCQ3afwhak5myRw==";
            _rsaConfig.PrivateKey = string.Empty;
            // When
            var response = _cryptService.Decrypt(textEncrypted, _rsaConfig);
            
            // Then
            Assert.False(response.IsSucceed);
            Assert.True(string.IsNullOrEmpty(response.Text));
        }

        [Fact]
        public void Decrypt_WithPubicKeyEmpty_ReturnSucceed()
        {
            // Given
            string text = "This is my test";
            string textEncrypted = "VK5284DVyRGKCSmkHVq9jWiWti9TcWuzUu6vLOD+uXHURuuKzsMKSkA5T5i7v+9MD0PRxtBqMw7VZb4eeVCKfJKpi7PSlPC8WasrifcBta2cG4949kF0HDEVK5i5c2Y1Us0XmjitSUltRERZwrpmoBOvIOxeDtK5ivF7oSQFbellS1pBBB4Ysqr6ZUsLhcuNctRqezSmZHu3U82XVZShVt3bEQynRI8+lLeGW/kesSJPaXsEuWyf8ShG+Xuvvf1ynLvcnjI2c8Prq0SF8WBFcqUwVIvT6LVzz6OYipG2gkegMInYnqu/ozqL5LfV/MgMvRxQ91ogCQ3afwhak5myRw==";
            _rsaConfig.PublicKey = string.Empty;
            // When
            var response = _cryptService.Decrypt(textEncrypted, _rsaConfig);
            
            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Text));
            Assert.Equal(text,response.Text);
        }

        [Fact]
        public void Decrypt_WithCorrectParameters_ReturnSucceed()
        {
            // Given
            string text = "This is my test";
            string textEncrypted = "VK5284DVyRGKCSmkHVq9jWiWti9TcWuzUu6vLOD+uXHURuuKzsMKSkA5T5i7v+9MD0PRxtBqMw7VZb4eeVCKfJKpi7PSlPC8WasrifcBta2cG4949kF0HDEVK5i5c2Y1Us0XmjitSUltRERZwrpmoBOvIOxeDtK5ivF7oSQFbellS1pBBB4Ysqr6ZUsLhcuNctRqezSmZHu3U82XVZShVt3bEQynRI8+lLeGW/kesSJPaXsEuWyf8ShG+Xuvvf1ynLvcnjI2c8Prq0SF8WBFcqUwVIvT6LVzz6OYipG2gkegMInYnqu/ozqL5LfV/MgMvRxQ91ogCQ3afwhak5myRw==";
            
            // When
            var response = _cryptService.Decrypt(textEncrypted, _rsaConfig);
            
            // Then
            Assert.True(response.IsSucceed);
            Assert.True(!string.IsNullOrEmpty(response.Text));
            Assert.Equal(text,response.Text);
        }
    }
}