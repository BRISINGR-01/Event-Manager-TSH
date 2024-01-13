using System.Security.Cryptography;

namespace Logic.Configuration
{
    public class HashingConfig
    {
        public static string Section = "hashing";
        private string hashAlgorithmName = "SHA512";
        public string HashAlgorithmName
        {
            get => hashAlgorithmName; set
            {
                hashAlgorithmName = value;
                HashAlgorithm = new HashAlgorithmName(value);
            }
        }
        public string Pepper { get; set; } = string.Empty;
        public HashAlgorithmName HashAlgorithm { get; private set; }
        public HashingConfig()
        {
            HashAlgorithm = new HashAlgorithmName(hashAlgorithmName);
        }
        public HashingConfig(string pepper, string? hashAlgorithmName = null)
        {
            if (hashAlgorithmName != null)
            {
                this.hashAlgorithmName = hashAlgorithmName;
            }

            HashAlgorithmName = this.hashAlgorithmName;
            Pepper = pepper;
        }
    }
}
