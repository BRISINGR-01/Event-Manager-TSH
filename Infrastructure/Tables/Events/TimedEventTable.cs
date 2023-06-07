using MySql.Data.MySqlClient;
using Logic;
using Infrastructure.Tables.Interfaces;

namespace Infrastructure.Tables.Events
{
    public class TimedEventTable : ITable
    {
        public string TableName { get => "timed_event"; }

        public static readonly string EventId = "event_id";
        public static readonly string Start = "start";
        public static readonly string End = "end";
    }
}
