using Shared.Errors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Events
{
    public class PaidEvent: TimedEvent
    {
        public double Price { get; private set; }
        public int? MaxParticipants { get; private set; }
        public int? Percentage { get; private set; }
        public PaidEvent(Guid id, Guid branchId, string title, string description, DateTime start, DateTime? end, string? venue, double price, int? maxParticipants): base(id, branchId, title, description, start, end, venue)
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
