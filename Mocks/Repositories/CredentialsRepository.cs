using Logic.Interfaces.Repositories;
using Logic.Models;

namespace Mocks.Repositories
{
    public class CredentialsRepository : MockRepository<Credentials>, ICredentialsRepository
    {
        public CredentialsRepository() : base() { }
        public Credentials? GetByEmail(string email) => _data.Find(c => c.Email == email);

    }
}
