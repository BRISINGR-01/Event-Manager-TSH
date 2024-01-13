using Logic.Interfaces.Repositories;
using Logic.Models;
using Shared;
using Shared.Enums;

namespace Mocks.Repositories
{
    public class UserRepository : MockRepository<User>, IUserRepository
    {
        private List<PushNotificationSubscription> subscriptions = new();
        public UserRepository() : base()
        {
            AddData(new(
                MockData.UserIds[0],
                MockData.BranchIds[0],
                "John Doe",
                UserRole.Administrator
            ));
            AddData(new(
                MockData.UserIds[1],
                MockData.BranchIds[0],
                "Jane Doe",
                UserRole.Guest
            ));
            AddData(new(
                Helpers.NewGuid,
                MockData.BranchIds[0],
                "Event Organizer",
                UserRole.EventOrganizer
            ));
            Create(new(
                Helpers.NewGuid,
                MockData.BranchIds[0],
                "Event Organizer",
                UserRole.EventOrganizer
            ));
        }
        public List<User> GetBranchContacts(Guid branchId)
        {
            return _data.Where(u => u.BranchId == branchId && (u.Role == UserRole.EventOrganizer || u.Role == UserRole.StudentComitee)).ToList();
        }
        public List<User> GetByRoles(List<UserRole> roles, Guid branchId)
        {
            return _data.Where(u => u.BranchId == branchId && roles.Contains(u.Role)).ToList();
        }
        public List<User> GetAllFromBranch(Guid? branchId = null)
        {
            return _data.Where(u => branchId == null || u.BranchId == branchId).ToList();
        }

        public bool Subscribe(PushNotificationSubscription subscription)
        {
            subscriptions.Add(subscription);
            return true;
        }
        public List<PushNotificationSubscription> GetSubscriptions(List<UserRole> roles, Guid branchId) => subscriptions;
    }
}
