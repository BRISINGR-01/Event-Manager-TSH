using System.Security.Cryptography;

namespace Shared
{
    public static class Constants
    {
        public static readonly string LAST_LOGGED_KEY = "lastLoggedUserId";

        public static readonly int KEY_LENGTH = 16; //128 bits
        public static readonly int ITERATIONS = 1000;
        public static readonly HashAlgorithmName METHOD = HashAlgorithmName.SHA384;
        public static readonly int OFFSET_LIMIT_VALUE = 10;

        public static string TokenCookie = "token";
    }
}
