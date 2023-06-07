using Logic;
using Logic.Interfaces.Repositories;
using Logic.Interfaces.Repositories.Events;
using Logic.Models;
using Logic.Models.Events;
using Logic.Models.Images;
using Unit_testing.Repositories;
using Unit_testing.Repositories.Events;

namespace Unit_testing
{
    public static class MockRepository
    {
        public static readonly IUserRepository User = new UserRepository();
        public static readonly IEventRepository Event = new EventRepository();
        public static readonly IBranchRepository Branch = new BranchRepository();
        public static readonly IImageRepository Image = new ImageRepository();

        public static User UserFirst { get => new UserRepository().GetAll()[0]; }
        public static Event EventFirst { get => new EventRepository().GetAll(BranchFirst.Id)[0]; }
        public static Branch BranchFirst { get => new BranchRepository().GetAll()[0]; }
        public static Image ImageFirst { get => new ImageRepository().GetAll()[0]; }
    }
}
