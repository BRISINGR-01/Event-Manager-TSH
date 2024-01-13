using Infrastructure.Tables.Events;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using Shared.Enums;

namespace Infrastructure.Repositories.Events
{
    public class ParticipanceRepository : DatabaseRepository, IEventParticipanceRepository
    {
        public ParticipanceRepository(DatabaseManager db) : base(db, EventParticipanceTable.TableName) { }
        public List<EventParticipance> GetAll(int? offsetIndex = null)
        {
            return sql.Select
                .All
                .Get<EventParticipance>();
        }
        public List<EventParticipance> GetAllForEvent(Guid eventId)
        {
            return sql.Select
                .All
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .FinishSelect
                .Get<EventParticipance>();
        }
        public int GetAllSignedForEvent(Guid eventId)
        {
            return sql.Select
                .All
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .And
                .Where(EventParticipanceTable.State).Equals(EventParticipanceEnum.Signed)
                .FinishSelect
                .Count;
        }
        public bool Create(EventParticipance participance)
        {
            return sql.Insert
                .Set(EventParticipanceTable.Id, Guid.NewGuid())
                .Set(EventParticipanceTable.UserId, participance.UserId)
                .Set(EventParticipanceTable.EventId, participance.EventId)
                .Set(EventParticipanceTable.State, participance.State)
                .Execute();
        }
        public bool Update(EventParticipance participance)
        {
            return sql.Update
             .Set(EventParticipanceTable.UserId, participance.UserId)
             .Set(EventParticipanceTable.EventId, participance.EventId)
             .Set(EventParticipanceTable.State, participance.State)
             .Where(EventParticipanceTable.Id).Equals(participance.Id)
             .Execute();
        }
        public bool Delete(Guid id)
        {
            return sql.Delete
                .Where(EventParticipanceTable.Id).Equals(id)
                .Execute();
        }
        public bool DeleteAllOfEvent(Guid eventId)
        {
            sql.Delete
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .Execute();

            return true;
        }
        public EventParticipance? GetById(Guid id)
        {
            return sql.Select
                .All
                .Where(EventParticipanceTable.Id).Equals(id)
                .FinishSelect
                .First<EventParticipance>();
        }
        public EventParticipance? GetEventUserParticipance(Guid eventId, Guid userId)
        {
            return sql.Select
                .All
                .Where(EventParticipanceTable.UserId).Equals(userId)
                .And
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .FinishSelect
                .First<EventParticipance>();
        }
    }
}
