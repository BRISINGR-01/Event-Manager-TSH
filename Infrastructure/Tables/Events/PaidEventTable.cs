namespace Infrastructure.Tables.Events
{
    public static class PaidEventTable
    {
        public static readonly string TableName = "paid_event";
        public static readonly string EventId = "timed_event_id";
        public static readonly string Price = "price";
        public static readonly string MaxParticipants = "max_participants";
    }
}
