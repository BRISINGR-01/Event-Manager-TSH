using Infrastructure.Tables;
using Logic.Interfaces.Repositories;
using Logic.Models;
using Shared;

namespace Infrastructure.Repositories
{
    public class CredentialsRepository : DatabaseRepository, ICredentialsRepository
    {
        public CredentialsRepository(DatabaseManager db) : base(db, CredentialsTable.TableName) { }
        public Credentials? GetById(Guid id)
        {
            return sql.Select
                .All
                .Where(CredentialsTable.Id).Equals(id)
                .FinishSelect
                .First<Credentials>();
        }
        public List<Credentials> GetAll(int? offsetIndex = null)
        {
            return sql.Select.All.Get<Credentials>();
        }
        public bool Create(Credentials entity)
        {
            return sql.Insert
                .Set(CredentialsTable.Id, entity.Id == Guid.Empty ? Helpers.NewGuid : entity.Id)
                .Set(CredentialsTable.Email, entity.Email)
                .Set(CredentialsTable.PasswordHash, entity.PasswordHash)
                .Set(CredentialsTable.Salt, entity.Salt)
                .Execute();
        }
        public bool Update(Credentials entity)
        {
            return sql.Update
                .Set(CredentialsTable.PasswordHash, entity.PasswordHash)
                .Set(CredentialsTable.Salt, entity.Salt)
                .Where(CredentialsTable.Email).Equals(entity.Email)
                .Execute();
        }
        public bool Delete(Guid id)
        {
            return sql.Delete.Where(CredentialsTable.Id).Equals(id).Execute();
        }
        public Credentials? GetByEmail(string email)
        {
            return sql.Select
                .All
                .Where(CredentialsTable.Email).Equals(email)
                .FinishSelect
                .First<Credentials>();
        }
    }
}
