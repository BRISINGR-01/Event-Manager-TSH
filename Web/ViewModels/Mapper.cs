using Logic.Models.Events;

namespace Web.ViewModels
{
    public static class Mapper
    {
        public static Event Map(this EventViewModel @event)
        {
            if (@event.Start != null)
            {
                if (@event.Price != null)
                {
                    return new PaidEvent(
                        @event.Id, 
                        @event.BranchId, 
                        @event.Title,
                        @event.Description,
                        (DateTime)@event.Start,
                        @event.End,
                        @event.Venue,
                        (double)@event.Price,
                        @event.MaxParticipants
                    );
                }

                return new TimedEvent(
                    @event.Id,
                    @event.BranchId,
                    @event.Title,
                    @event.Description,
                    (DateTime)@event.Start,
                    @event.End,
                    @event.Venue
                );
            }

            return new Logic.Models.Events.Event(@event.Id, @event.BranchId, @event.Title, @event.Description, @event.Venue);
        }
    }
}
