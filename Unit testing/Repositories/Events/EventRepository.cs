using Shared.Errors;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;

namespace Unit_testing.Repositories.Events
{
    public class EventRepository : MockRepository<Event>, IEventRepository
    {
        public IEventParticipanceRepository Participance { get; private set; }

        public EventRepository() : base()
        {
            AddData(new PaidEvent(Guid.Parse("62d3ff53-ad22-42b5-a56f-2c94fb819996"), Guid.Parse("a228fdea-8417-4157-bed9-e43a9e86b59a"), "Language Exchange", "Language Exchange", DateTime.MinValue, DateTime.MaxValue, "TSH", 10, 1));
            Participance = new ParticipanceRepository();
        }
        new public List<Event> GetAll(int? offsetIndex = null) => _data;
        public List<Event> GetAll(Guid branchId)
        {
            return base.GetAll().Where(e => e.BranchId == branchId).ToList();
        }
        public List<Event> GetOnGoing()
        {
            return base.GetAll().Where(e => e.BranchId == BranchId).ToList();
        }
        new public bool Create(Event @event)
        {
            return base.Create(@event);
        }
        public List<Event> GetManyBy(string title) => throw new NotImplementedException("You must specify a branch");
        public List<Event> GetMonthlyEvents(int monthOffset)
        {
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            var lastDayOfMonth = new DateTime(date.Year, date.Month + monthOffset, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59);
            
            return _data.Where(e => e is TimedEvent timedEvent && e.BranchId == BranchId && timedEvent.Start > firstDayOfMonth && timedEvent.Start < lastDayOfMonth).ToList();
        }
    }
}
