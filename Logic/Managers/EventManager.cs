﻿using Logic.Managers;
using Logic;
using Logic.Models;
using Shared.Errors;
using Shared.Enums;
using Logic.Interfaces.Repositories.Events;
using Logic.Models.Events;
using System.Globalization;
using Shared;

namespace Domain.Managers
{
    public class EventManager: BaseManager<Event, IEventRepository>
    {
        public ParticipanceManager Participance { get; private set; }
        public EventManager(IEventRepository repository, IEventParticipanceRepository participanceRepository, IdentityUser user) : base(repository, user)
        {
            Participance = new(participanceRepository, user);
        }
        public new Result Create(Event @event)
        {
            var res = Result<bool>.From(() =>
            {
                @event.Validate();
                return  VerifiedRepository(UserRole.EventOrganizer).Create(@event);
            }, CRUD.CREATE);
             
            return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
        }
        public new Result Update(Event @event)
        {
            var res = Result<bool>.From(() => {
                @event.Validate(isUpdate: true);
                return VerifiedRepository(UserRole.EventOrganizer).Update(@event);
            }, CRUD.UPDATE);
             
            return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
        }
        public Result<List<Event>> GetAllOfMonth(int month = 0)
        {
            return Result<List<Event>>.From(() => VerifiedRepository().GetMonthlyEvents(month));
        }
        public Result<Event?> GetBy(Guid id)
        {
            return Result<Event?>.From(() => {
                var @event = VerifiedRepository().FindSingleBy(id);

                if (@event is PaidEvent paidEvent && paidEvent.MaxParticipants != null)
                {
                    paidEvent.SetIsFullyBooked(IsFullyBooked(id, paidEvent.MaxParticipants));
                    return paidEvent;
                }

                return @event;
            });
        }
        public Result<bool> AlterParticipance(Guid userId, Guid eventId, EventParticipanceEnum eventParticipance)
        {
            var participance = VerifiedRepository().Participance.GetEventUserParticipance(eventId, userId);
            if (participance == null)
            {
                if (eventParticipance != EventParticipanceEnum.Signed) return Result<bool>.FailWith("You are not signed up yet");

                if (IsFullyBooked(eventId)) return Result<bool>.FailWith("Unfortunately the event is fully booked! You can try again later.");
                
                return Result<bool>.From(() => VerifiedRepository().Participance.Create(new(Helpers.NewGuid, eventId, userId, eventParticipance)));
            }

            if (eventParticipance == EventParticipanceEnum.None) return Result<bool>.From(() => VerifiedRepository().Participance.Delete(participance.Id));

            
            return Result<bool>.From(() => VerifiedRepository().Participance.Update(new(participance.Id, participance.EventId, participance.UserId, eventParticipance)));            
        }
        private bool IsFullyBooked(Guid eventId, int? maxParticipants = null)
        {
            if (maxParticipants == null)
            {
                var res = GetBy(eventId);

                if (!res.IsSuccessful || res.Value == null) return false;

                var @event = res.Value;

                if (@event is PaidEvent paidEvent)
                {
                    maxParticipants = paidEvent.MaxParticipants;
                }
                else
                {
                    return false;
                }
            }

            if (maxParticipants <= 0) return false;

            var resPart = Participance.GetAllSignedForEvent(eventId);

            return resPart.IsSuccessful && resPart.Value >= maxParticipants;
        }
        public Result<List<Event>> GetOnGoing()
        {
            return Result<List<Event>>.From(() => VerifiedRepository().GetOnGoing());
        }
        public Result<Dictionary<string, int>> GetCountEventsPerMonth()
        {
            List<string> monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();

            return Result<Dictionary<string, int>>.From(() => {
                Dictionary<string, int> res = new()
                {
                    { "Other", 0 }
                };
                foreach (var e in VerifiedRepository().GetAll())
                {
                    if (e is TimedEvent timedEvent) {
                        string month = monthNames[timedEvent.Start.Month == -1 ? 11 : timedEvent.Start.Month - 1];
                        if (!res.ContainsKey(month))
                        {
                            res.Add(month, 0);
                        }

                        res[month] += 1;
                    } else
                    {
                        res["Other"] += 1 ;
                    }
                }

                return res;
            });
        }
    }
}