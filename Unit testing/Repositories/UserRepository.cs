using Logic;
using Shared.Enums;
using Logic.Interfaces.Repositories;
using Logic.Models;

namespace Unit_testing.Repositories
{
    public class UserRepository : MockRepository<User>, IUserRepository
    {
        public UserRepository() : base()
        {
            AddData(new(
                Guid.Parse("97694444-91be-472d-acd8-650139dcf9b8"),
                Guid.Parse("9d1acbca-640b-48cb-9421-adf9a863f9bd"),
                "John Doe",
                "password",
                UserRole.Administrator,
                "email@gmail.com"
            ));
            AddData(new(
                Guid.Parse("9d1acbca-640b-48cb-9421-adf9a863f9bd"),
                Guid.Parse("9d1acbca-640b-48cb-9421-adf9a863f9bd"),
                "Jane Doe",
                "password",
                UserRole.Guest,
                "email@gmail.com"
            ));
        }
        public User? GetBy(Guid id)
        {
            return _data.Find(x => x.Id == id);
        }
        public bool Exists(User user) => _data.Any(u => user.Email == u.Email);
        public User? FindSingleBy(string email, string password) => _data.Find(u => u.Email == email && u.Password == password);
        public List<User> GetBranchContacts()
        {
            return _data.Where(u => u.BranchId == BranchId && (u.Role == UserRole.EventOrganizer || u.Role == UserRole.StudentComitee)).ToList();
        }
        public List<User> FindManyBy(Guid? branchId = null)
        {
            return _data.Where(u => branchId == null || u.BranchId == branchId).ToList();
        }
    }
}
