using Logic.Interfaces.Repositories.Base;
using Logic.Models;
using Shared.Enums;

namespace Logic.Interfaces.Repositories
{

    public interface IUserRepository : IDbRepository<User>
    {
        public List<User> GetBranchContacts(Guid branchId);
        public List<User> GetAllFromBranch(Guid? branchId = null);
        public bool Subscribe(PushNotificationSubscription subscription);
        public List<User> GetByRoles(List<UserRole> roles, Guid branchId);
        public List<PushNotificationSubscription> GetSubscriptions(List<UserRole> roles, Guid branchId);
    }
}
