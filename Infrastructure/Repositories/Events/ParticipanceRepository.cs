using Shared.Enums;
using Infrastructure.DatabaseManagers;
using Infrastructure.Tables.Events;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;

namespace Infrastructure.Repositories.Events
{
    public class ParticipanceRepository : DatabaseInstance<EventParticipanceTable>, IEventParticipanceRepository
    {
        public Guid BranchId { get => branchId; }
        public ParticipanceRepository(string connectionString, Guid branchId) : base(connectionString, branchId) { }
        public List<EventParticipance> GetAll(int? offsetIndex = null) {
            return sql.Select
                .All
                .Get<EventParticipance>();
        }
        public List<EventParticipance> GetAllForEvent(Guid eventId)
        {
            return sql.Select
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .FinishSelect
                .Get<EventParticipance>();
        }
        public int GetAllSignedForEvent(Guid eventId)
        {
            return sql.Select
                .Count
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .And
                .Where(EventParticipanceTable.State).Equals(EventParticipanceEnum.Signed)
                .FinishSelect
                .CountValue;
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
        public EventParticipance? FindSingleBy(Guid id)
        {
            return sql.Select
                .Where(EventParticipanceTable.Id).Equals(id)
                .FinishSelect
                .First<EventParticipance>();
        }
        public List<EventParticipance> FindManyBy(Guid EventId)
        {
            return sql.Select
                .Where(EventParticipanceTable.EventId).Equals(EventId)
                .FinishSelect
                .Get<EventParticipance>();
        }
        public EventParticipance? GetEventUserParticipance(Guid eventId, Guid userId)
        {
            return sql.Select
                .Where(EventParticipanceTable.UserId).Equals(userId)
                .And
                .Where(EventParticipanceTable.EventId).Equals(eventId)
                .FinishSelect
                .First<EventParticipance>();
        }
    }
}
