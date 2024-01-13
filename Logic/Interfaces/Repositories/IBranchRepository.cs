using Logic.Interfaces.Repositories.Base;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    
    public interface IBranchRepository: IDbRepository<Branch>
    {
        public List<Branch> FindManyBy(string branchName);
    }
}
