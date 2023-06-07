using MySql.Data.MySqlClient;
using Logic;
using Infrastructure.Tables.Interfaces;

namespace Infrastructure.Tables.Events
{
    public class PaidEventTable : ITable
    {
        public string TableName { get => "paid_event"; }

        public static readonly string EventId = "event_id";
        public static readonly string Price = "price";
        public static readonly string MaxParticipants = "max_participants";
    }
}
