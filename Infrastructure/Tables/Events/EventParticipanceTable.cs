using MySql.Data.MySqlClient;
using Logic;
using Shared.Enums;
using Shared.Errors;
using Infrastructure.Tables.Interfaces;

namespace Infrastructure.Tables.Events
{
    public class EventParticipanceTable : ITableWithEnums
    {
        public string TableName { get => "event_participance"; }

        public static readonly string Id = "id";
        public static readonly string UserId = "user_id";
        public static readonly string EventId = "event_id";
        public static readonly string State = "state";

        public static string EnumToSQLValue(Enum state)
        {
            return state switch
            {
                EventParticipanceEnum.None => "none",
                EventParticipanceEnum.Present => "present",
                EventParticipanceEnum.Signed => "signed",
                _ => throw DeveloperExceptions.InvalidEnum(state),
            };
        }
        string ITableWithEnums.EnumToSQLValue(Enum role)
        {
            return EnumToSQLValue(role);
        }
    }
}
