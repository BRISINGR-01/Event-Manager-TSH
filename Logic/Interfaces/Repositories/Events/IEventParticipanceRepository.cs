using Logic.Models.Events;

namespace Logic.Interfaces.Repositories.Events
{
    public interface IEventParticipanceRepository : IRepository<EventParticipance>
    {
        public EventParticipance? GetEventUserParticipance(Guid eventId, Guid userId);
        public bool DeleteAllOfEvent(Guid eventId);
        public int GetAllSignedForEvent(Guid eventId);
    }
}
