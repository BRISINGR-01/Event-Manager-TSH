using Infrastructure.Tables.Events;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using Shared;
using Shared.Enums;
using SQL_Query_Builder.Select;

namespace Infrastructure.Repositories.Events
{
    public class EventRepository : DatabaseRepository, IEventRepository
    {
        public EventRepository(DatabaseManager db) : base(db, EventTable.TableName) { }
        private AfterOnColumns events => sql.Select
                .All
                .Join(TimedEventTable.TableName, JoinType.Left).OnColumns(EventTable.Id, TimedEventTable.EventId)
                .Join(PaidEventTable.TableName, JoinType.Left).OnColumns(EventTable.Id, PaidEventTable.EventId);

        public List<Event> GetAll(int? offsetIndex = null)
        {
            return events
                .OrderBy(TimedEventTable.Start)
                .Get<Event>();
        }
        public List<Event> GetSuggestions(Guid branchId)
        {
            return events
                .Where(EventTable.IsSuggestion).Equals(true)
                .FinishSelect
                .Get<Event>();
        }
        public bool Create(Event @event)
        {
            Guid id = @event.Id == Guid.Empty ? Helpers.NewGuid : @event.Id;

            var res = sql.Insert
                .Set(EventTable.Id, id)
                .Set(EventTable.BranchId, @event.BranchId)
                .Set(EventTable.Title, @event.Title)
                .Set(EventTable.Description, @event.Description)
                .Set(EventTable.Venue, @event.Venue)
                .Set(EventTable.IsSuggestion, @event.IsSuggestion)
                .Execute();

            if (@event is TimedEvent timedEvent)
            {
                res &= sql.FromTable(TimedEventTable.TableName)
                    .Insert
                    .Set(TimedEventTable.EventId, id)
                    .Set(TimedEventTable.Start, timedEvent.Start)
                    .Set(TimedEventTable.End, timedEvent.End)
                    .Execute();
            }

            if (@event is PaidEvent paidEvent)
            {
                res &= sql.FromTable(PaidEventTable.TableName)
                    .Insert
                    .Set(PaidEventTable.EventId, id)
                    .Set(PaidEventTable.Price, paidEvent.Price)
                    .Set(PaidEventTable.MaxParticipants, paidEvent.MaxParticipants)
                    .Execute();
            }

            return res;
        }
        public bool Update(Event @event)
        {
            var res =
                sql.Update
                .Set(EventTable.BranchId, @event.BranchId)
                .Set(EventTable.Title, @event.Title)
                .Set(EventTable.Description, @event.Description)
                .Set(EventTable.Venue, @event.Venue)
                .Where(EventTable.Id).Equals(@event.Id)
                .Execute();


            if (@event is TimedEvent timedEvent)
            {
                res &= sql.FromTable(TimedEventTable.TableName)
                    .Update
                    .Set(TimedEventTable.Start, timedEvent.Start)
                    .Set(TimedEventTable.End, timedEvent.End)
                    .Where(TimedEventTable.EventId).Equals(@event.Id)
                    .Execute();
            }

            if (@event is PaidEvent paidEvent)
            {
                bool exists = sql.FromTable(PaidEventTable.TableName)
                        .Select
                        .All
                        .Where(PaidEventTable.EventId).Equals(@event.Id)
                        .FinishSelect
                        .Count != 0;

                if (exists)
                {
                    res &= sql.FromTable(PaidEventTable.TableName)
                        .Update
                        .Set(PaidEventTable.Price, paidEvent.Price)
                        .Set(PaidEventTable.MaxParticipants, paidEvent.MaxParticipants)
                        .Where(PaidEventTable.EventId).Equals(@event.Id)
                        .Execute();
                }
                else
                {
                    res &= sql.FromTable(PaidEventTable.TableName)
                    .Insert
                    .Set(PaidEventTable.EventId, @event.Id)
                    .Set(PaidEventTable.Price, paidEvent.Price)
                    .Set(PaidEventTable.MaxParticipants, paidEvent.MaxParticipants)
                    .Execute();
                }
            }

            return res;
        }
        public bool Delete(Guid id)
        {
            var res = sql.Delete
                .Where(EventTable.Id).Equals(id)
                .Execute();

            sql.FromTable(TimedEventTable.TableName)
                .Delete
                .Where(TimedEventTable.EventId).Equals(id)
                .Execute();

            sql.FromTable(PaidEventTable.TableName)
                .Delete
                .Where(PaidEventTable.EventId).Equals(id)
                .Execute();

            return res;
        }
        public Event? GetById(Guid id)
        {
            return events
                .Where($"{EventTable.TableName}.{EventTable.Id}").Equals(id)
                .FinishSelect
                .First<Event>();
        }
        public List<Event> GetMonthlyEvents(int monthOffset, Guid branchId, EventsFilterEnum? filter)
        {
            var now = DateTime.Now;
            int year = now.Year;
            int month = now.Month + monthOffset;
            while (month <= 0)
            {
                year--;
                month += 11;
            }

            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);

            return events
                .Where(EventTable.BranchId).Equals(branchId)
                .And
                .Where(TimedEventTable.Start).IsMoreOrEqual(firstDayOfMonth)
                .And
                .Where(TimedEventTable.Start).IsLessOrEqual(lastDayOfMonth)
                .FinishSelect
                .OrderBy(TimedEventTable.Start)
                .Get<Event>();
        }
        public List<Event> GetOnGoing(Guid branchId)
        {
            var end = DateTime.Now;
            var start = end.AddMinutes(-30);

            return events
                .Where(EventTable.BranchId).Equals(branchId)
                .And
                .Where(TimedEventTable.Start).IsLessOrEqual(start)
                .And
                .Where(TimedEventTable.End).IsMoreOrEqual(end)
                .FinishSelect
                .OrderBy(TimedEventTable.Start)
                .Get<Event>();
        }
    }
}
