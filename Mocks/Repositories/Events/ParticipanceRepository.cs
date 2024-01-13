using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using Shared;
using Shared.Enums;

namespace Mocks.Repositories.Events
{
    public class ParticipanceRepository : MockRepository<EventParticipance>, IEventParticipanceRepository
    {
        public ParticipanceRepository() : base()
        {
            AddData(new EventParticipance(Helpers.NewGuid, MockData.EventIds[0], MockData.UserIds[0], EventParticipanceEnum.Signed));
        }
        new public List<EventParticipance> GetAll(int? offsetIndex = null) => _data;
        public int GetAllSignedForEvent(Guid eventId)
        {
            return _data.Where(e => e.EventId == eventId && (e.State == EventParticipanceEnum.Signed || e.State == EventParticipanceEnum.Present)).Count();
        }
        public bool DeleteAllOfEvent(Guid eventId)
        {
            _data.RemoveAt(_data.FindIndex(e => e.EventId == eventId));
            return true;
        }
        public EventParticipance? GetEventUserParticipance(Guid eventId, Guid userId)
        {
            return _data.Find(e => e.EventId == eventId && e.UserId == userId);
        }
    }
}
