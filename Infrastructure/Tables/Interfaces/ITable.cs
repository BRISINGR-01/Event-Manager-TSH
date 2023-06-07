using Logic.Interfaces;
using MySql.Data.MySqlClient;

namespace Infrastructure.Tables.Interfaces
{
    public interface ITable
    {
        public string TableName { get; }
    }
}
