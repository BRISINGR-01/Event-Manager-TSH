using Shared.Enums;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;

namespace Unit_testing.Repositories.Events
{
    public class ParticipanceRepository : MockRepository<EventParticipance>, IEventParticipanceRepository
    {
        public ParticipanceRepository() : base()
        {
            AddData(new EventParticipance(Guid.Parse("6d51543d-b368-42a2-9c42-f04cfa1cf597"), Guid.Parse("62d3ff53-ad22-42b5-a56f-2c94fb819996"), Guid.Parse("97694444-91be-472d-acd8-650139dcf9b8"), EventParticipanceEnum.Signed));
        }
        new public List<EventParticipance> GetAll(int? offsetIndex = null) => _data;
        public List<EventParticipance> GetAllForEvent(Guid eventId)
        {
            return _data.Where(e => e.EventId == eventId).ToList();
        }
        public int GetAllSignedForEvent(Guid eventId)
        {
            return _data.Where(e => e.EventId == eventId && (e.State == EventParticipanceEnum.Signed || e.State == EventParticipanceEnum.Present)).Count();
        }
        public bool DeleteAllOfEvent(Guid eventId)
        {
            _data.RemoveAt(_data.FindIndex(e => e.EventId == eventId));
            return true;
        }
        public List<EventParticipance> FindManyBy(Guid eventId)
        {
            return _data.Where(e => e.EventId == eventId).ToList();
        }
        public EventParticipance? GetEventUserParticipance(Guid eventId, Guid userId)
        {
            return _data.Find(e => e.EventId == eventId && e.UserId == userId);
        }
    }
}
