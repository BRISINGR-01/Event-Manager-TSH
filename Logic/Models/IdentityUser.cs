using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public bool IsEventOrganizer { get => Role == UserRole.EventOrganizer; }
        public bool IsStudentComitee { get => Role == UserRole.StudentComitee; }
    }
}
