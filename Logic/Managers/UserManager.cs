using Logic.Managers;
using Logic;
using Logic.Interfaces.Repositories;
using Shared.Enums;
using Shared.Errors;
using Logic.Models;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using Logic.Interfaces;

namespace Domain.Managers
{
    public class UserManager : BaseManager<User, IUserRepository>
    {
        public UserManager(IUserRepository repository, IdentityUser user) : base(repository, user) {
            
        }
        public Result<List<User>> GetBranchContacts(Guid? branchId = null)
        {
            return Result<List<User>>.From(() => VerifiedRepository().GetBranchContacts());
        }
        public Result<List<User>> GetAll(Guid? branchId = null)
        {
            return Result<List<User>>.From(() => VerifiedRepository(UserRole.Administrator | UserRole.EventOrganizer).FindManyBy(branchId));
        }

        public override Result Create(User user)
        {
            if (VerifiedRepository().Exists(user))
            {
                return Result.FailWith("A user with this email already exists");
            } else
            {
                var res = Result<bool>.From(() =>
                {
                    user.Validate();
                    return VerifiedRepository(UserRole.Administrator).Create(user);
                }, CRUD.CREATE, "user");

                return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
            }
        }
        public Result<User?> GetById(Guid id)
        {
            return Result<User?>.From(() => VerifiedRepository().GetBy(id));
        }
        public static Result<User?> Get(IUserRepository repository, Guid id)
        {
            return Result<User?>.From(() => repository.FindSingleBy(id));
        }
        public static Result<User?> CheckCredentials(IUserRepository repository, string email, string password)
        {
            return Result<User?>.From(() => repository.FindSingleBy(email, password));
        }
    }
}