using Logic.Models;

namespace Logic.Interfaces.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        public User? GetBy(Guid id);
        public bool Exists(User user);
        public User? FindSingleBy(string email, string password);
        public List<User> GetBranchContacts();
        public List<User> FindManyBy(Guid? branchId = null);
    }
}
