using Domain.Managers;
using Logic.Interfaces;
using Logic.Models;
using Mocks.Repositories;
using Shared.Enums;

namespace Unit_testing
{
    public class Manager : IManager
    {
        private IdentityUser _user;
        public bool IsMocked { get; set; }
        public UserManager User { get; set; }
        public EventManager Event { get; set; }
        public BranchManager Branch { get; set; }
        public ImageManager Image { get; set; }
        public CredentialsManager Credentials { get; set; }
        public LocalDataManager Local { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Manager()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Reset();
        }
        public void SetUserRole(UserRole role)
        {
            _user = new IdentityUser(_user.Id, _user.BranchId, role);
        }
        public void Reset()
        {
            User user = MockRepositories.UserFirst;
            _user = new IdentityUser(user.Id, user.BranchId, user.Role);

            User = new(MockRepositories.User, MockRepositories.Credentials);
            Event = new(MockRepositories.Event, MockRepositories.Participance);
            Branch = new(MockRepositories.Branch);
            Image = new(MockRepositories.Image);
            Credentials = new(MockRepositories.Credentials);
        }
    }
}
