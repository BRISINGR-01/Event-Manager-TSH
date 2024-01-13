using Domain.Managers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Events;
using Logic.Interfaces;
using Mocks.Repositories;

namespace Infrastructure
{
    public class Manager : IManager
    {
        private readonly DatabaseManager databaseManager;
        public UserManager User { get; private set; }
        public EventManager Event { get; private set; }
        public BranchManager Branch { get; private set; }
        public ImageManager Image { get; private set; }
        public LocalDataManager Local { get; private set; }
        public CredentialsManager Credentials { get; private set; }
        public bool IsMocked { get; private set; }
        public Manager()
        {
            databaseManager = new DatabaseManager();
            IsMocked = !databaseManager.HealthCheck();

            if (IsMocked)
            {
                User = new(MockRepositories.User, MockRepositories.Credentials);
                Event = new(MockRepositories.Event, MockRepositories.Participance);
                Branch = new(MockRepositories.Branch);
                Image = new(MockRepositories.Image);
                Local = new(MockRepositories.Local);
                Credentials = new(MockRepositories.Credentials);
            }
            else
            {
                User = new(new Infrastructure.Repositories.UserRepository(databaseManager), new Infrastructure.Repositories.CredentialsRepository(databaseManager));
                Event = new(new EventRepository(databaseManager), new ParticipanceRepository(databaseManager));
                Branch = new(new Infrastructure.Repositories.BranchRepository(databaseManager));
                Credentials = new(new Infrastructure.Repositories.CredentialsRepository(databaseManager));
                Image = new(new Infrastructure.Repositories.Images.ImageRepository());
                Local = new(new LocalRepository());
            }
        }
    }
}
