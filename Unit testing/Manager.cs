using Domain.Managers;
using Logic;
using Logic.Models;
using Logic.Models.Events;
using Shared.Enums;

namespace Unit_testing
{
    public class Manager
    {
        private IdentityUser _user;
        public UserManager User;
        public EventManager Event;
        public BranchManager Branch;
        public ImageManager Image;
        public Manager()
        {
            User user = MockRepository.UserFirst;
            _user = new IdentityUser(user.Id, user.BranchId, user.Role);

            User = new(MockRepository.User, _user);
            Event = new(MockRepository.Event, MockRepository.Event.Participance, _user);
            Branch = new(MockRepository.Branch, _user);
            Image = new(MockRepository.Image, _user);

        }
        public void SetUserRole(UserRole role)
        {
            _user = new IdentityUser(_user.Id, _user.BranchId, role);
        }

    }
}
