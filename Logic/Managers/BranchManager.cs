using Logic.Managers;
using Logic;
using Logic.Interfaces.Repositories;
using Shared.Errors;
using Logic.Models;
using Shared.Enums;

namespace Domain.Managers
{
    public class BranchManager: BaseManager<Branch, IBranchRepository>
    {
        public BranchManager(IBranchRepository repository, IdentityUser user): base(repository, user)  { } 

        public Result<List<Branch>> GetByName(string name)
        {
            return Result<List<Branch>>.From(() => VerifiedRepository(UserRole.Administrator).FindManyBy(name));
        }
    }
}