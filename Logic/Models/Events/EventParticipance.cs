using Logic.Interfaces;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Events
{
    public class EventParticipance : IEntity
    {
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public Guid UserId { get; private set; }
        public EventParticipanceEnum State { get; private set; }

        public EventParticipance(Guid id, Guid eventId, Guid userId, EventParticipanceEnum participance)
        {
            Id = id;
            EventId = eventId;
            UserId = userId;
            State = participance;
        }
    }
}
