using Logic.Configuration;
using Logic.Interfaces;
using Logic.Utilities;
using Shared;
using Shared.Errors;
using System.Text.RegularExpressions;

namespace Logic.Models
{
    public class Credentials : IEntity
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        private HashingUtility? _hashingUtility;
        public Credentials(string email, string plainPassword, HashingConfig config, Guid? userId = null)
        {
            this.Id = userId ?? Helpers.NewGuid;
            Configure(config);

            var (salt, passwordhash) = _hashingUtility!.CreateSaltAndHash(plainPassword);

            Salt = salt;
            PasswordHash = passwordhash;
            Email = email;
        }
        public Credentials(Guid userId, string email, string passwordHash, string salt)
        {
            Id = userId;
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
        }
        public void Configure(HashingConfig config)
        {
            _hashingUtility = new HashingUtility(config);
        }
        public bool VerifyPassword(string password)
        {
            return _hashingUtility!.CreateHash(password, Salt) == PasswordHash;
        }
        public void ValidateEmail()
        {
            if (!Regex.IsMatch(Email, @"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"))
            {
                throw new ClientException("The email is invalid");
            }
        }
    }
}
