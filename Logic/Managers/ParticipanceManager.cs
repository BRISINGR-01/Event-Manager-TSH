using Logic.Interfaces.Repositories.Events;
using Logic.Models;
using Logic.Models.Events;
using Shared.Enums;

namespace Logic.Managers
{
    public class ParticipanceManager: BaseManager<EventParticipance, IEventParticipanceRepository>
    {
        public ParticipanceManager(IEventParticipanceRepository repository, IdentityUser user) : base(repository, user) { }

        public Result<EventParticipance?> GetEventUserState(Guid eventId, Guid userId)
        {
            return Result<EventParticipance?>.From(() => VerifiedRepository().GetEventUserParticipance(eventId, userId));
        }

        public Result<int> GetAllSignedForEvent(Guid eventId)
        {
            return Result<int>.From(() => VerifiedRepository().GetAllSignedForEvent(eventId));
        }
        public Result<Dictionary<Guid, Dictionary<EventParticipanceEnum, int>>> GetUserStatistics()
        {
            return Result<Dictionary<Guid, Dictionary<EventParticipanceEnum, int>>>.From(() => {
                Dictionary<Guid, Dictionary<EventParticipanceEnum, int>> res = new();
                
                foreach (var participance in VerifiedRepository().GetAll())
                {
                    Dictionary<EventParticipanceEnum, int> stats;
                    if (!res.ContainsKey(participance.UserId))
                    {
                        stats = new();
                        res.Add(participance.UserId, stats);
                    } else
                    {
                        stats = res[participance.UserId];
                    }

                    if (!stats.ContainsKey(participance.State)) stats.Add(participance.State, 0);

                    stats[participance.State] += 1;
                }

                return res;
            });
        }
    }
}
