using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using Logic.Utilities;
using Shared.Enums;

namespace Logic.Managers
{

    public class ParticipanceManager : BaseDbManager<EventParticipance>
    {
        private IEventParticipanceRepository participanceRepository;
        public ParticipanceManager(IEventParticipanceRepository repository) : base(repository)
        {
            participanceRepository = repository;
        }

        public Result<EventParticipance> GetEventUserParticipance(Guid eventId, Guid userId)
        {
            return Result<EventParticipance>.From(() => participanceRepository.GetEventUserParticipance(eventId, userId));
        }

        public Result<int> GetAllSignedForEvent(Guid eventId)
        {
            return Result<int>.From(() => participanceRepository.GetAllSignedForEvent(eventId));
        }

        public Result<Dictionary<Guid, Dictionary<EventParticipanceEnum, int>>> GetUserStatistics()
        {
            return Result<Dictionary<Guid, Dictionary<EventParticipanceEnum, int>>>.From(() =>
            {
                Dictionary<Guid, Dictionary<EventParticipanceEnum, int>> res = new();

                foreach (var participance in participanceRepository.GetAll())
                {
                    Dictionary<EventParticipanceEnum, int> stats;
                    if (!res.ContainsKey(participance.UserId))
                    {
                        stats = new();
                        res.Add(participance.UserId, stats);
                    }
                    else
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
