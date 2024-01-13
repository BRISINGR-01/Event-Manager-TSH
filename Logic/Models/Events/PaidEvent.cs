using Logic.Utilities;
using Shared.Errors;

namespace Logic.Models.Events
{

    public class PaidEvent : TimedEvent
    {
        public double Price { get; private set; }
        public int? MaxParticipants { get; private set; }
        public int? Percentage { get; private set; }
        public static PaidEvent New(TimedEvent @event, double price, int? maxParticipants) => new(@event.Id, @event.BranchId, @event.Title, @event.Description, @event.Start, @event.End, @event.Venue, price, maxParticipants, @event.IsSuggestion);
        public PaidEvent(Guid id, Guid branchId, string title, string description, DateTime start, DateTime? end, string? venue, double price, int? maxParticipants, bool isSuggestion) : base(id, branchId, title, description, start, end, venue, isSuggestion)
        {
            Price = price;
            MaxParticipants = maxParticipants;
        }

        public override void Validate(bool isUpdate = false)
        {
            if (Price <= 0) throw new ClientException("Cannot create a paid event which doesn't cost money!");
            if (MaxParticipants != null && MaxParticipants < 0) MaxParticipants = null;
        }


        public override void SetSigned(Result<int> res)
        {
            base.SetSigned(res);
            if (Signed != 0 && Signed != null && MaxParticipants != null && MaxParticipants != 0) Percentage = 100 * Signed / MaxParticipants;
        }
    }
}
