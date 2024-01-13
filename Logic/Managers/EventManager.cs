using Logic.Interfaces.Repositories.Events;
using Logic.Managers;
using Logic.Models.Events;
using Logic.Utilities;
using Shared.Enums;
using Shared.Errors;
using System.Globalization;

namespace Domain.Managers
{

    public class EventManager : BaseDbManager<Event>
    {
        private new readonly IEventRepository repository;
        public ParticipanceManager Participance { get; private set; }
        public EventManager(IEventRepository repository, IEventParticipanceRepository participanceRepository) : base(repository)
        {
            this.repository = repository;
            Participance = new(participanceRepository);
        }
        public override Result Create(Event @event)
        {
            return Result.From(() =>
            {
                @event.Validate();
                if (!repository.Create(@event)) throw new ClientException("Couldn't create Event");
            });
        }
        public Result<List<Event>> GetSuggestions(Guid branchId)
        {
            return Result<List<Event>>.From(() => repository.GetSuggestions(branchId));
        }

        public override Result Update(Event @event)
        {
            return Result.From(() =>
            {
                @event.Validate(isUpdate: true);
                base.Update(@event);
            });
        }
        public Result<List<Event>> GetAllOfMonth(int month, Guid branchId, EventsFilterEnum? filter)
        {
            return Result<List<Event>>.From(() => repository.GetMonthlyEvents(month, branchId, filter));
        }
        public Result<Event> GetBy(Guid id)
        {
            return Result<Event>.From(() =>
            {
                var @event = repository.GetById(id);

                if (@event is PaidEvent paidEvent)
                {
                    paidEvent.SetIsFullyBooked(IsFullyBooked(id, paidEvent.MaxParticipants));
                }

                return @event;
            });
        }
        public Result AlterParticipance(Guid userId, Guid eventId, EventParticipanceEnum eventParticipance)
        {
            var participanceRes = Participance.GetEventUserParticipance(eventId, userId);
            if (participanceRes.IsUnSuccessful)
            {
                if (eventParticipance != EventParticipanceEnum.Signed) return Result.FailWith("You are not signed up yet");

                if (IsFullyBooked(eventId)) return Result.FailWith("Unfortunately the event is fully booked! You can try again later.");

                return Participance.Create(EventParticipance.New(eventId, userId, eventParticipance));
            }

            if (eventParticipance == EventParticipanceEnum.None)
            {
                return Participance.Delete(participanceRes.Value.Id);
            }

            return Participance.Update(new(participanceRes.Value.Id, participanceRes.Value.EventId, participanceRes.Value.UserId, eventParticipance));
        }
        private bool IsFullyBooked(Guid eventId, int? maxParticipants = null)
        {
            if (maxParticipants == null)
            {
                var res = GetBy(eventId);

                if (res.IsUnSuccessful) return false;

                var @event = res.Value;

                if (@event is not PaidEvent paidEvent) return false;

                maxParticipants = paidEvent.MaxParticipants;
            }

            if (maxParticipants <= 0) return false;

            var resPart = Participance.GetAllSignedForEvent(eventId);

            return resPart.IsSuccessful && resPart.Value >= maxParticipants;
        }
        public Result<List<Event>> GetOnGoing(Guid branchId)
        {
            return Result<List<Event>>.From(() => repository.GetOnGoing(branchId));
        }
        public Result<Dictionary<string, int>> GetCountEventsPerMonth()
        {
            return Result<Dictionary<string, int>>.From(GenerateStatistics);
        }

        private Dictionary<string, int> GenerateStatistics()
        {
            List<string> monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();

            Dictionary<string, int> res = new()
                {
                    { "Other", 0 }
                };

            foreach (var e in repository.GetAll())
            {
                string month = "Other";
                if (e is TimedEvent timedEvent)
                {
                    month = monthNames[timedEvent.Start.Month == -1 ? 11 : timedEvent.Start.Month - 1];
                    if (!res.ContainsKey(month)) res.Add(month, 0);
                }

                res[month] += 1;
            }

            return res;
        }
    }
}