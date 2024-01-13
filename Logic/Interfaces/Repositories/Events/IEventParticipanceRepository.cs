using Logic.Interfaces.Repositories.Base;
using Logic.Models.Events;

namespace Logic.Interfaces.Repositories.Events
{

    public interface IEventParticipanceRepository : IDbRepository<EventParticipance>
    {
        public EventParticipance? GetEventUserParticipance(Guid eventId, Guid userId);
        public bool DeleteAllOfEvent(Guid eventId);
        public int GetAllSignedForEvent(Guid eventId);
    }
}
