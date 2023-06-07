using Infrastructure.SQL;
using Infrastructure.Tables;
using Logic;
using Shared;
using Logic.Interfaces.Repositories;
using Logic.Models;

namespace Infrastructure.DatabaseManagers
{
    public class BranchRepository : DatabaseInstance<BranchTable>, IBranchRepository
    {
        public Guid BranchId { get => branchId; }
        public BranchRepository(string connectionString, Guid branchId) : base(connectionString, branchId) { }
        public List<Branch> GetAll(int? offsetIndex = null)
        {
            return sql.Select
                .All
                .OrderBy(BranchTable.Name)
                .Get<Branch>();
        }
        public bool Create(Branch Branch)
        {
            return sql.Insert
                .Set(BranchTable.Id, Helpers.NewId)
                .Set(BranchTable.Name, Branch.Name)
                .Execute();
        }
        public bool Update(Branch Branch)
        {
            return sql.Update
                .Set(BranchTable.Name, Branch.Name)
                .Where(BranchTable.Id).Equals(Branch.Id)
                .Execute();
        }
        public bool Delete(Guid id) 
        { 
            return sql.Delete
                .Where(BranchTable.Id).Equals(id)
                .Execute();
        }
        public Branch? FindSingleBy(Guid id)
        {
            return sql.Select
                .All
                .Where(BranchTable.Id).Equals(id)
                .FinishSelect
                .First<Branch>();
        }
        public List<Branch> FindManyBy(string name)
        {
            return sql.Select
                .All
                .Where(BranchTable.Name).Contains(name)
                .OrderBy(BranchTable.Name)
                .Get<Branch>();
        }
    }
}
