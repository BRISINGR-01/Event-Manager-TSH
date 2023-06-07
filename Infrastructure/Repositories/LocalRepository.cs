using System.Text.Json.Nodes;
using System.Text.Json;
using MySql.Data.MySqlClient;
using System.Configuration;
using Logic.Interfaces.Repositories;

namespace Infrastructure.DatabaseManagers
{
    public class LocalRepository: ILocalRepository
    {
        public void Update(string key, string? value)
        {
            try
            {
                ConfigurationManager.AppSettings.Set(key, value);
            }
            catch { }
        }

        public string? Read(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(key);
            } catch {
                return null;
            }
        }
    }
}
