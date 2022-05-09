using Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Bl
{
    public class PasswordService : IPasswordService
    {
        public string CalculateHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentOutOfRangeException(nameof(password));
            }

            var salt = new byte[128 / 8];
            using (var rngCsp = RandomNumberGenerator.Create())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
