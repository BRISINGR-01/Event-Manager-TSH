namespace Infrastructure.Tables.Events
{
    public static class EventParticipanceTable
    {
        public static readonly string TableName = "event_participance";

        public static readonly string Id = "id";
        public static readonly string UserId = "user_id";
        public static readonly string EventId = "event_id";
        public static readonly string State = "state";
    }
}
