using MySql.Data.MySqlClient;
using Logic;
using Infrastructure.Tables.Interfaces;

namespace Infrastructure.Tables.Events
{
    public class EventTable : ITableWithId
    {
        public string TableName { get => "event"; }

        public static readonly string Id = "id";
        public static readonly string BranchId = "branch_id";
        public static readonly string Title = "title";
        public static readonly string Description = "description";
        public static readonly string Venue = "venue";
    }
}
