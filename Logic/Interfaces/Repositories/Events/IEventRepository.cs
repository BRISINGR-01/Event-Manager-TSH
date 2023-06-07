using Logic.Models.Events;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories.Events
{
    public interface IEventRepository : IRepository<Event>
    {
        public IEventParticipanceRepository Participance { get; }
        public List<Event> GetManyBy(string name);
        public List<Event> GetMonthlyEvents(int monthOffset);
        public List<Event> GetOnGoing();
    }
}
