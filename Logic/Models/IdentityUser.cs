using Shared.Enums;

namespace Logic.Models
{
    public class IdentityUser
    {
        public Guid Id { get; private set; }
        public Guid BranchId { get; private set; }
        public UserRole Role { get; private set; }

        public IdentityUser(Guid id, Guid branchId, UserRole role)
        {
            Id = id;
            BranchId = branchId;
            Role = role;
        }
        public IdentityUser() { }
        protected void Setup(Guid id, Guid branchId, UserRole role)
        {
            Id = id;
            BranchId = branchId;
            Role = role;
        }

        public bool IsEventOrganizer => Role == UserRole.EventOrganizer;
        public bool IsStudentComitee => Role == UserRole.StudentComitee;

        public static IdentityUser Default => new(id: Guid.Empty, branchId: Guid.Empty, UserRole.Guest);
    }
}
