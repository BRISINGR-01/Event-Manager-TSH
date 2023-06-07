using Infrastructure.SQL;
using Infrastructure.Tables;
using Logic;
using Shared;
using Logic.Interfaces.Repositories;
using Unit_testing.Repositories;
using Logic.Models;

namespace Unit_testing.Repositories
{
    public class BranchRepository : MockRepository<Branch>, IBranchRepository
    {
        public BranchRepository(): base()
        {
            AddData(new Branch(Guid.Parse("a228fdea-8417-4157-bed9-e43a9e86b59a"), "Amsterdam"));
            AddData(new Branch(Guid.Parse("bb114576-334a-4c96-8b44-ee3966ca1f46"), "Eindhoven"));
        }
        public List<Branch> FindManyBy(string name)
        {
            return _data.FindAll(branch => branch.Name.Contains(name));
        }
    }
}
