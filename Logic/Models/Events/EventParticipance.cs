using Logic.Interfaces;
using Shared;
using Shared.Enums;

namespace Logic.Models.Events
{

    public class EventParticipance : IEntity
    {
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public Guid UserId { get; private set; }
        public EventParticipanceEnum State { get; private set; }

        public static EventParticipance New(Guid eventId, Guid userId, EventParticipanceEnum participance) => new(Helpers.NewGuid, eventId, userId, participance);
        public EventParticipance(Guid id, Guid eventId, Guid userId, EventParticipanceEnum participance)
        {
            Id = id;
            EventId = eventId;
            UserId = userId;
            State = participance;
        }
    }
}
