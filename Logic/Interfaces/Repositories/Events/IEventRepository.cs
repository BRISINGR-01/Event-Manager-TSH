using Logic.Interfaces.Repositories.Base;
using Logic.Models.Events;
using Shared.Enums;

namespace Logic.Interfaces.Repositories.Events
{

    public interface IEventRepository : IDbRepository<Event>
    {
        public List<Event> GetSuggestions(Guid branchId);
        public List<Event> GetMonthlyEvents(int monthOffset, Guid branchId, EventsFilterEnum? filter);
        public List<Event> GetOnGoing(Guid branchId);
    }
}
