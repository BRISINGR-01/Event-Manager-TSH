using Logic.Interfaces;
using MySql.Data.MySqlClient;

namespace Infrastructure.Tables.Interfaces
{
    public interface ITableWithId: ITable
    {
        public static string Id { get => throw new NotImplementedException("Every class implementing ITable must override the static Id property"); }
    }
}
