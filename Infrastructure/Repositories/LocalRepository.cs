using Logic.Interfaces.Repositories;
using System.Configuration;

namespace Infrastructure.Repositories
{
    public class LocalRepository : IConfigurationRepository
    {
        public bool Update(string key, string? value)
        {
            ConfigurationManager.AppSettings.Set(key, value);

            return true;
        }

        public string? Read(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
