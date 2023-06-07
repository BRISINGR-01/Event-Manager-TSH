using Shared.Enums;
using Shared;
using Logic.Interfaces;
using Logic.Models;
using System.Text.RegularExpressions;
using Shared.Errors;

namespace Logic.Models
{
    public class User : IdentityUser, IEntity
    {
        public new Guid Id { get; private set; }
        public new Guid BranchId { get; private set; }
        public new UserRole Role { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string? Image { get => LocalPath.ToRelative(LocalPath.FindUserImagePath(BranchId, Id)); }
        public string Email { get; private set; }
        public User(
            Guid id,
            Guid branchId,
            string userName,
            string password,
            UserRole role,
            string email
        ) : base(id, branchId, role)
        {
            Id = id;
            BranchId = branchId;
            UserName = userName;
            Password = password;
            Role = role;
            Email = email;
        }
        public bool IsEventResponsible { get => Role == UserRole.StudentComitee || Role == UserRole.EventOrganizer; }
        public string FirstName { get => UserName.Split(" ")[0]; }
        public string LastName { get => UserName.Contains(' ') ? UserName.Split(" ")[1] : string.Empty; }
        public override string ToString()
        {
            return UserName;
        }

        public void Validate()
        {
            if (!Regex.IsMatch(Email, @"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"))
            {
                throw new ClientException("The email is invalid");
            } else if (UserName.Length <  2)
            {
                throw new ClientException("The username is too short");
            } else if (Password.Length < 5)
            {
                throw new ClientException("Please provide a stronger password");
            }
        }
    }
}
