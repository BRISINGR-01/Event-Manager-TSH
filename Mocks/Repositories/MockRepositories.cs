using Logic.Interfaces.Repositories;
using Logic.Interfaces.Repositories.Events;
using Logic.Interfaces.Repositories.Images;
using Logic.Models;
using Logic.Models.Events;
using Logic.Models.Images;
using Mocks.Repositories.Events;

namespace Mocks.Repositories
{
    public static class MockRepositories
    {
        public static readonly IUserRepository User = new UserRepository();
        public static readonly IEventRepository Event = new EventRepository();
        public static readonly IBranchRepository Branch = new BranchRepository();
        public static readonly ICredentialsRepository Credentials = new CredentialsRepository();
        public static readonly IImageRepository Image = new ImageRepository();
        public static readonly IEventParticipanceRepository Participance = new ParticipanceRepository();
        public static readonly IConfigurationRepository Local = new LocalDataRepository();

        public static User UserFirst => new UserRepository().GetAll()[0];
        public static Event EventFirst => new EventRepository().GetAll(BranchFirst.Id)[0];
        public static Branch BranchFirst => new BranchRepository().GetAll()[0];
        public static Image ImageFirst => new ImageRepository().GetAll()[0];
    }
}
