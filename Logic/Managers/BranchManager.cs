using Logic.Interfaces.Repositories;
using Logic.Managers;
using Logic.Models;

namespace Domain.Managers
{

    public class BranchManager : BaseDbManager<Branch>
    {
        public BranchManager(IBranchRepository repository) : base(repository) { }
    }
}