using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Logic
{
    public class Encryption
    {
        private readonly byte[] salt;
        private readonly string token;
        private byte[] IV =
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };
        public Encryption()
        {
            salt = new byte[] { 19, 62, 161, 92, 236, 255, 224, 30, 238, 221, 160, 238, 160, 110, 240, 158, 104, 250, 16, 88, 166, 165, 114, 167, 219, 33, 213, 167, 153, 255, 201, 150 };
            token = "3trldyYMxfX86jiOwWJF7HhIot0pLslP";
        }
        private byte[] DeriveKeyFromPassword(string password)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                Encoding.Latin1.GetBytes(password),
                salt,
                1000,
                HashAlgorithmName.SHA256,
                16
            );
        }

        public string? Encrypt(string clearText)
        {
            using Aes aes = Aes.Create();

            aes.Key = DeriveKeyFromPassword(token);
            aes.IV = IV;

            using MemoryStream output = new();
            try
            {
                using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

                cryptoStream.Write(Encoding.Latin1.GetBytes(clearText));
                cryptoStream.FlushFinalBlock();

                return Encoding.Latin1.GetString(output.ToArray());
            } catch
            {
                return null;
            }
        }

        public string? Decrypt(string? encrypted)
        {
            if (encrypted == null) return null;

            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(token);
            aes.IV = IV;

            try
            {
                using MemoryStream input = new(Encoding.Latin1.GetBytes(encrypted));
                using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

                using MemoryStream output = new();
                
                cryptoStream.CopyTo(output);
                return Encoding.Latin1.GetString(output.ToArray());
            } catch
            {
                return null;
            }
        }
    }
}

