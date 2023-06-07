using Infrastructure.Tables;
using Logic;
using Shared.Enums;
using Shared;
using Logic.Interfaces.Repositories;
using Shared.Errors;
using Infrastructure.Repositories;
using Logic.Models;

namespace Infrastructure.DatabaseManagers
{
    public class UserRepository : DatabaseInstance<UserTable>, IUserRepository
    {
        public Guid BranchId { get => branchId; }
        private readonly ImageRepository imgRepository;
        public UserRepository(string connectionString, Guid? branchId) : base(connectionString, branchId ?? Guid.Empty)
        {
            imgRepository = new ImageRepository(connectionString, branchId ?? Guid.Empty);
        }
        public User? GetBy(Guid id)
        {
            return sql.Select
                .All
                .Where(UserTable.Id).Equals(id)
                .FinishSelect
                .First<User>();
        }public List<User> GetAll(int? offsetIndex = null)
        {
            return sql.Select
                .All
                .OrderBy(UserTable.UserName)
                .Get<User>();
        }
        public bool Create(User user)
        {
            return sql.Insert
                .Set(UserTable.Id, Helpers.NewId)
                .Set(UserTable.BranchId, user.BranchId)
                .Set(UserTable.UserName, new Encryption().Encrypt(user.UserName))
                .Set(UserTable.Password, new Encryption().Encrypt(user.Password))
                .Set(UserTable.Role, user.Role)
                .Set(UserTable.Email, new Encryption().Encrypt(user.Email))
                .Execute();
        }
        public bool Exists(User user)
        {
            return sql
                .Select
                .Count
                .Where(UserTable.Email).Equals(new Encryption().Encrypt(user.Email))
                .FinishSelect
                .CountValue > 0;
        }
        public bool Update(User user)
        {
            return sql.Update
                .Set(UserTable.UserName, new Encryption().Encrypt(user.UserName))
                .Set(UserTable.Password, new Encryption().Encrypt(user.Password))
                .Set(UserTable.Role, user.Role)
                .Set(UserTable.Email, new Encryption().Encrypt(user.Email))
                .Where(UserTable.Id).Equals(user.Id)
                .Execute();
        }
        public bool Delete(Guid id)
        {
            try
            {
                imgRepository.DeleteType(id, ImageType.User);
            } catch { }

            return
                sql.Delete
                .Where(UserTable.Id).Equals(id)
                .Execute();
        }
        public User? FindSingleBy(Guid id)
        {
            return sql.Select
                .All
                .Where(UserTable.Id).Equals(id)
                .FinishSelect.First<User>();
        }
        public User? FindSingleBy(string email, string password)
        {
            return sql.Select
                .All
                .Where(UserTable.Email).Equals(new Encryption().Encrypt(email))
                .And
                .Where(UserTable.Password).Equals(new Encryption().Encrypt(password))
                .FinishSelect
                .First<User>();
        }
        public List<User> GetBranchContacts()
        {
            return sql.Select
                .All
                .Where(UserTable.Role).Equals(UserRole.EventOrganizer)
                .Or
                .Where(UserTable.Role).Equals(UserRole.StudentComitee)
                .And
                .Where(UserTable.BranchId).Equals(branchId)
                .OrderBy(UserTable.UserName)
                .Get<User>();
        }
        public List<User> FindManyBy(Guid? branchId = null)
        {
            if (branchId == null)
            {
                return sql.Select
                 .All
                 .OrderBy(UserTable.UserName)
                 .Get<User>();
            }
            else
            {
                return sql.Select
                    .All
                    .Where(UserTable.BranchId).Equals(branchId)
                    .OrderBy(UserTable.UserName)
                    .Get<User>();
            }
        }
    }
}
