using MySql.Data.MySqlClient;
using Infrastructure.Tables;
using Logic.Interfaces;
using Infrastructure.SQL;
using Logic;
using Logic.Models.Images;
using Logic.Interfaces.Repositories;
using Infrastructure.Tables.Interfaces;

namespace Infrastructure.DatabaseManagers
{
    public abstract class DatabaseInstance<T> where T : ITable, new()
    {
        protected Guid branchId;
        protected readonly SQLQueryGenerator<T> sql;
        public DatabaseInstance(string connectionString, Guid branchId)
        {
            sql = new SQLQueryGenerator<T>(new MySqlConnection(connectionString));
            this.branchId = branchId;
        }
    }
}
