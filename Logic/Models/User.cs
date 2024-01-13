using Logic.Interfaces;
using Logic.Utilities;
using Shared.Enums;
using Shared.Errors;

namespace Logic.Models
{

    public class User : IdentityUser, IEntity
    {
        public new Guid Id { get; private set; }
        public new Guid BranchId { get; private set; }
        public new UserRole Role { get; private set; }
        public string UserName { get; private set; }
        public string? Image => LocalPath.FindImagePath(Id, BranchId, ImageType.User);
        public User(
            Guid id,
            Guid branchId,
            string userName,
            UserRole role
        ) : base(id, branchId, role)
        {
            Id = id;
            BranchId = branchId;
            UserName = userName;
            Role = role;
        }
        public bool IsEventResponsible => Role == UserRole.StudentComitee || Role == UserRole.EventOrganizer;
        public string FirstName => UserName.Split(" ")[0];
        public string LastName => UserName.Contains(' ') ? UserName.Split(" ")[1] : string.Empty;
        public override string ToString()
        {
            return UserName;
        }
        public void Validate()
        {
            if (UserName.Length < 2)
            {
                throw new ClientException("The username is too short");
            }
        }
    }
}
