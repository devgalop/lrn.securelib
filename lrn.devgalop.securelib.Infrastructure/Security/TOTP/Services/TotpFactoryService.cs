// Original class taken from https://github.com/alicommit-malp/Easy-Totp
// Copyright (c) [2021] alicommit-malp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Models;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Interfaces;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Models;

namespace lrn.devgalop.securelib.Infrastructure.Security.TOTP.Services
{
    public class TotpFactoryService : ITotpFactoryService
    {
        private readonly EpochTicks _epochTicksValues;
        private readonly TotpParameters _totpParameters;
        private readonly AesCryptType _aesConfig;
        private readonly IAesCryptService _cryptService;

        public TotpFactoryService(EpochTicks epochTicksValues,
                                    TotpParameters totpParameters,
                                    AesCryptType aesConfig,
                                    IAesCryptService cryptService)
        {
            _totpParameters = totpParameters;
            _aesConfig = aesConfig;
            _cryptService = cryptService;
            _epochTicksValues = epochTicksValues;
        }

        public TotpResponse Compute()
        {
            try
            {
                ValidParameters();

                var window = CalculateTimeStepFromTimestamp(DateTime.UtcNow);

                var data = GetBigEndianBytes(window);

                var hmac = new HMACSHA1 { Key = _aesConfig.Key };
                var hmacComputedHash = hmac.ComputeHash(data);

                var offset = hmacComputedHash[hmacComputedHash.Length - 1] & 0x0F;
                var otp = (hmacComputedHash[offset] & 0x7f) << 24
                          | (hmacComputedHash[offset + 1] & 0xff) << 16
                          | (hmacComputedHash[offset + 2] & 0xff) << 8
                          | (hmacComputedHash[offset + 3] & 0xff) % 1000000;

                var result = Digits(otp, _totpParameters.Size);

                return new()
                {
                    IsSucceed = true,
                    TotpCode = result
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }

        }

        public TotpResponse ComputeEncrypted()
        {
            try
            {
                if(_cryptService is null) throw new ArgumentNullException("To encrypt is mandatory inject the dependency IAesCryptService");
                var computeResult = Compute();
                if (!computeResult.IsSucceed) throw new Exception(computeResult.ErrorMessage);
                var encryptionResult = _cryptService.Encrypt(computeResult.TotpCode, _aesConfig);
                if (!encryptionResult.IsSucceed) throw new Exception(encryptionResult.ErrorMessage);
                return new()
                { 
                    IsSucceed = true,
                    TotpCode = encryptionResult.Text ?? computeResult.TotpCode
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.ToString()
                };
            }

        }

        private byte[] GetBigEndianBytes(long input)
        {
            // Since .net uses little endian numbers, we need to reverse the byte order to get big endian.
            var data = BitConverter.GetBytes(input);
            Array.Reverse(data);
            return data;
        }

        private long CalculateTimeStepFromTimestamp(DateTime timestamp)
        {
            var unixTimestamp = (timestamp.Ticks - _epochTicksValues.UnixEpochTicks) / _epochTicksValues.TicksToSeconds;
            var window = unixTimestamp / _totpParameters.ValidFor;
            return window;
        }

        private string Digits(long input, int digitCount)
        {
            var truncatedValue = ((int)input % (int)Math.Pow(10, digitCount));
            return truncatedValue.ToString().PadLeft(digitCount, '0');
        }

        private void ValidParameters()
        {
            if(_totpParameters is null) throw new Exception("Totp parameters cannot be null");
            if(_totpParameters.Size <= 0) throw new ArgumentException("Totp parameter size cannot be negative number");
            if(_totpParameters.ValidFor <= 0) throw new ArgumentException("Totp parameter valid for cannot be negative number");
            if(_aesConfig is null)throw new Exception("Aes configuration cannot be null");
            if(string.IsNullOrEmpty(_aesConfig.KeyValue)) throw new ArgumentException("Aes key cannot be null or empty");
            if(_epochTicksValues is null)throw new Exception("Epoch ticks constants cannot be null");
        }
    }
}