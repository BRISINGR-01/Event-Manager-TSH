using Shared.Errors;

namespace Logic.Models.Events
{
    public class TimedEvent: Event
    {
        public DateTime Start { get; private set; }
        public DateTime? End { get; private set; }
        public TimedEvent(Guid id, Guid branchId, string title, string description, DateTime start, DateTime? end, string? venue): base(id, branchId, title, description, venue)
        {
            Start = start;
            End = end;
        }

        public override void Validate(bool isUpdate = false)
        {
            if (End < Start) End = null;
            if (!isUpdate && End < DateTime.Now) throw new ClientException("You cannot make an event end before you create it!");
        }
    }
}
