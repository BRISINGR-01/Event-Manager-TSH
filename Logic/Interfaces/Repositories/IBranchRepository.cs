using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface IBranchRepository: IRepository<Branch>
    {
        public List<Branch> FindManyBy(string branchName);
    }
}
