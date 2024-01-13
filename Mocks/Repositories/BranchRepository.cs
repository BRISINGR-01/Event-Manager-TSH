using Logic.Interfaces.Repositories;
using Logic.Models;

namespace Mocks.Repositories
{
    public class BranchRepository : MockRepository<Branch>, IBranchRepository
    {
        public BranchRepository() : base()
        {
            AddData(new Branch(MockData.BranchIds[0], "Amsterdam"));
            AddData(new Branch(MockData.BranchIds[1], "Eindhoven"));
        }
        public List<Branch> FindManyBy(string name)
        {
            return _data.FindAll(branch => branch.Name.Contains(name));
        }
    }
}
