using Shared.Errors;
using SQL_Query_Builder;
using System.Configuration;

namespace Infrastructure
{
    public class DatabaseManager
    {
        private string connectionString;
        private EntityFactory entityFactory => new();
        private EnumToStringConverter enumConverter => new();
        private SQLQueryBuilder GetSqlQueryBuilder() => new(connectionString, string.Empty, enumConverter, entityFactory);

        public DatabaseManager()
        {
            connectionString = GetConnectionString("Online");
        }

        public bool HealthCheck()
        {
            connectionString = GetConnectionString("Online");
            try
            {
                GetSqlQueryBuilder().HealthCheck();
                return true;
            }
            catch (Exception)
            {
                connectionString = GetConnectionString("Offline");
                try
                {
                    GetSqlQueryBuilder().HealthCheck();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public SQLQueryBuilder OfTable(string tableName) => GetSqlQueryBuilder().FromTable(tableName);
        private string GetConnectionString(string type)
        {
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[type] ?? throw DeveloperExceptions.CannotFindDatabaseCredentials;
                return settings.ConnectionString;
            }
            catch
            {
                throw DeveloperExceptions.CannotFindDatabaseCredentials;
            }
        }
    }
}
