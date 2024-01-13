using Logic.Interfaces.Repositories.Base;
using Logic.Models;

namespace Logic.Interfaces.Repositories
{

    public interface ICredentialsRepository : IDbRepository<Credentials>
    {
        public Credentials? GetByEmail(string email);
    }
}
