using Shared;
using Shared.Errors;
using Infrastructure.DatabaseManagers;
using Infrastructure.Tables.Events;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using Shared.Enums;

namespace Infrastructure.Repositories.Events
{
    public class EventRepository : DatabaseInstance<EventTable>, IEventRepository
    {
        private readonly ImageRepository imgRepository;
        public Guid BranchId { get => branchId; }
        public IEventParticipanceRepository Participance { get; private set; }

        public EventRepository(string connectionString, Guid branchId) : base(connectionString, branchId)
        {
            imgRepository = new ImageRepository(connectionString, branchId);
            Participance = new ParticipanceRepository(connectionString, branchId);
        }
        public List<Event> GetAll(int? offsetIndex = null)
        {
            return sql.Select
                .All
                .Join(new TimedEventTable()).OnColumns(EventTable.Id, TimedEventTable.EventId)
                .Join(new PaidEventTable()).OnColumns(EventTable.Id, PaidEventTable.EventId)
                .OrderBy(TimedEventTable.Start)
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
                .Execute();

            if (@event is TimedEvent timedEvent)
            {
                res &= sql.From<TimedEventTable>()
                    .Insert
                    .Set(TimedEventTable.EventId, id)
                    .Set(TimedEventTable.Start, timedEvent.Start)
                    .Set(TimedEventTable.End, timedEvent.End)
                    .Execute();
            }

            if (@event is PaidEvent paidEvent)
            {
                res &= sql.From<PaidEventTable>()
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
                res &= sql.From<TimedEventTable>()
                    .Update
                    .Set(TimedEventTable.Start, timedEvent.Start)
                    .Set(TimedEventTable.End, timedEvent.End)
                    .Where(TimedEventTable.EventId).Equals(@event.Id)
                    .Execute();
            }

            if (@event is PaidEvent paidEvent)
            {
                bool exists = sql.From<PaidEventTable>()
                        .Select
                        .Count
                        .Where(PaidEventTable.EventId).Equals(@event.Id)
                        .FinishSelect
                        .CountValue != 0;
                if (exists)
                {
                    res &= sql.From<PaidEventTable>()
                        .Update
                        .Set(PaidEventTable.Price, paidEvent.Price)
                        .Set(PaidEventTable.MaxParticipants, paidEvent.MaxParticipants)
                        .Where(PaidEventTable.EventId).Equals(@event.Id)
                        .Execute();
                }
                else
                {
                    res &= sql.From<PaidEventTable>()
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
            bool res;
            try
            {
                res = imgRepository.DeleteType(id, ImageType.Background) && Participance.DeleteAllOfEvent(id);
            } catch
            {
                res = false;
            }

            res &= sql.Delete
                .Where(EventTable.Id).Equals(id)
                .Execute();

            sql.From<TimedEventTable>()
                .Delete
                .Where(TimedEventTable.EventId).Equals(id)
                .Execute();
            
            sql.From<PaidEventTable>()
                .Delete
                .Where(PaidEventTable.EventId).Equals(id)
                .Execute();

            return res;
        }
        public Event? FindSingleBy(Guid id)
        {
            return sql.Select
                .All
                .Join(new TimedEventTable()).OnColumns(EventTable.Id, TimedEventTable.EventId)
                .Join(new PaidEventTable()).OnColumns(EventTable.Id, PaidEventTable.EventId)
                .Where($"{new EventTable().TableName}.{EventTable.Id}").Equals(id)
                .FinishSelect
                .First<Event>();
        }
        public List<Event> GetManyBy(string title) => throw new NotImplementedException("You must specify a branch");
        public List<Event> GetMonthlyEvents(int monthOffset)
        {
            var now = DateTime.Now;
            int month = now.Month + monthOffset;
            var firstDayOfMonth = new DateTime(now.Year, month, 1);
            var lastDayOfMonth = new DateTime(now.Year, month, DateTime.DaysInMonth(now.Year, month), 23, 59, 59);

            return sql.Select
                .All
                .Join(new TimedEventTable()).OnColumns(EventTable.Id, TimedEventTable.EventId)
                .Join(new PaidEventTable()).OnColumns(EventTable.Id, PaidEventTable.EventId)
                .Where(EventTable.BranchId).Equals(branchId)
                .And
                .Where(TimedEventTable.Start).IsMoreOrEqual(firstDayOfMonth)
                .And
                .Where(TimedEventTable.Start).IsLessOrEqual(lastDayOfMonth)
                .OrderBy(TimedEventTable.Start)
                .Get<Event>();
        }
        public List<Event> GetOnGoing()
        {
            //var end = DateTime.Now;
            var end =  new DateTime(2023, 05, 11, 18, 0, 0);
            //var start = DateTime.Now;
            var start = new DateTime(2023, 05, 11, 18, 0, 0).AddMinutes(30);
            
            return sql.Select
                .All
                .Join(new TimedEventTable()).OnColumns(EventTable.Id, TimedEventTable.EventId)
                .Join(new PaidEventTable()).OnColumns(EventTable.Id, PaidEventTable.EventId)
                .Where(EventTable.BranchId).Equals(branchId)
                .And
                .Where(TimedEventTable.Start).IsLessOrEqual(start)
                .And
                .Where(TimedEventTable.End).IsMoreOrEqual(end)
                .OrderBy(TimedEventTable.Start)
                .Get<Event>();
        }   
    }
}
