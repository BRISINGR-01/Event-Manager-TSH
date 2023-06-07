using Infrastructure.Tables.Interfaces;
using Logic;
using MySql.Data.MySqlClient;

namespace Infrastructure.Tables
{
    public class BranchTable: ITableWithId
    {
        public string TableName { get => "branch"; }

        public static readonly string Id = "id";
        public static readonly string Name = "branch_name";
    }
}
