using Logic.Interfaces.Repositories;

namespace Mocks.Repositories
{
    public class LocalDataRepository : IConfigurationRepository
    {
        public string Read(string key) => string.Empty;
        public bool Update(string key, string? val) => true;
    }
}
