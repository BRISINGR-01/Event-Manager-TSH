using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using Shared.Enums;

namespace Mocks.Repositories.Events
{
    public class EventRepository : MockRepository<Event>, IEventRepository
    {
        public IEventParticipanceRepository Participance { get; private set; }

        public EventRepository() : base()
        {
            AddData(new PaidEvent(MockData.EventIds[0], MockData.BranchIds[0], "Language Exchange", "Language Exchange", DateTime.MinValue, DateTime.MaxValue, "TSH", 10, 1, false));
            Participance = new ParticipanceRepository();
        }
        new public List<Event> GetAll(int? offsetIndex = null) => _data;
        public List<Event> GetAll(Guid branchId)
        {
            return base.GetAll().Where(e => e.BranchId == branchId).ToList();
        }
        public List<Event> GetSuggestions(Guid branchId)
        {
            return base.GetAll().Where(e => e.BranchId == branchId && e.IsSuggestion).ToList();
        }

        public List<Event> GetOnGoing(Guid branchId)
        {
            return base.GetAll().Where(e => e.BranchId == branchId).ToList();
        }
        override public bool Create(Event @event)
        {
            return base.Create(@event);
        }
        public List<Event> GetManyBy(string title) => throw new NotImplementedException("You must specify a branch");
        public List<Event> GetMonthlyEvents(int monthOffset, Guid branchId, EventsFilterEnum? filters)
        {
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            var lastDayOfMonth = new DateTime(date.Year, date.Month + monthOffset, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59);

            return _data.Where(e => e is TimedEvent timedEvent && e.BranchId == branchId && timedEvent.Start > firstDayOfMonth && timedEvent.Start < lastDayOfMonth).ToList();
        }
    }
}
