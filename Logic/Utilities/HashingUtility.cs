using Logic.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Logic.Utilities
{
    public class HashingUtility
    {
        private const int _keySize = 64;
        private const int _iterations = 350000;
        private readonly HashingConfig config;

        public HashingUtility(HashingConfig config)
        {
            this.config = config;
        }

        public (string Salt, string HashedPassword) CreateSaltAndHash(string password)
        {
            byte[] salt_bytes = RandomNumberGenerator.GetBytes(_keySize);
            string salt = Convert.ToHexString(salt_bytes);
            string hashedPassword = CreateHash(password, salt);
            return (salt, hashedPassword);
        }

        public string CreateHash(string password, string salt)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Convert.FromHexString(salt),
                _iterations,
                config.HashAlgorithm,
                _keySize);

            return Convert.ToHexString(hash);
        }
    }
}
