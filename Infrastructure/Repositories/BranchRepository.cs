using Infrastructure.Tables;
using Logic.Interfaces.Repositories;
using Logic.Models;
using Shared;

namespace Infrastructure.Repositories
{

    public class BranchRepository : DatabaseRepository, IBranchRepository
    {
        public BranchRepository(DatabaseManager db) : base(db, BranchTable.TableName) { }
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
        public Branch? GetById(Guid id)
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
                .FinishSelect
                .OrderBy(BranchTable.Name)
                .Get<Branch>();
        }
    }
}
