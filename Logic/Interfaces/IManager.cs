using Domain.Managers;

namespace Logic.Interfaces
{
    public interface IManager
    {
        public UserManager User { get; }
        public EventManager Event { get; }
        public BranchManager Branch { get; }
        public ImageManager Image { get; }
        public LocalDataManager Local { get; }
        public CredentialsManager Credentials { get; }
        public bool IsMocked { get; }
    }
}
