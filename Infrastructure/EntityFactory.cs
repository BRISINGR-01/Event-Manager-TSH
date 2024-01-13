using Infrastructure.Tables;
using Infrastructure.Tables.Events;
using Logic.Models;
using Logic.Models.Events;
using Logic.Utilities;
using Shared.Enums;
using Shared.Errors;
using SQL_Query_Builder.Interfaces;
using System.Reflection;

namespace Infrastructure
{
    public class EntityFactory : IEntityFactory
    {
        public T Create<T>(IDbDataReader reader)
        {
            var method = this.GetType().GetMethod("Create" + typeof(T).Name, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null) throw new DeveloperException($"A converter for {typeof(T).Name} is not implemented yet");

            try
            {
                return (T)method.Invoke(this, new object[] { reader })!;
            }
            catch
            {
                throw DeveloperExceptions.IncorrectData<T>(reader.GetString("id"));
            }
        }

        private User CreateUser(IDbDataReader reader)
        {
            return new User(
                id: reader.GetId(UserTable.Id),
                branchId: reader.GetId(UserTable.BranchId),
                userName: Encryption.Decrypt(reader.GetString(UserTable.UserName)),
                role: reader.GetString(UserTable.Role) switch
                {
                    "event_organizer" => UserRole.EventOrganizer,
                    "administrator" => UserRole.Administrator,
                    "student" => UserRole.Student,
                    "student_comitee" => UserRole.StudentComitee,
                    "guest" => UserRole.Guest,
                    _ => throw DeveloperExceptions.MissingEnumValue(typeof(UserRole))
                }
            );
        }
        private Branch CreateBranch(IDbDataReader reader)
        {
            return new Branch(
                id: reader.GetId(BranchTable.Id),
                name: reader.GetString(BranchTable.Name)
            );
        }
        private Event CreateEvent(IDbDataReader reader)
        {
            var @event = new Event(
                id: reader.GetId(EventTable.Id),
                branchId: reader.GetId(EventTable.BranchId),
                title: reader.GetString(EventTable.Title),
                description: reader.GetString(EventTable.Description),
                venue: reader.GetNullableString(EventTable.Venue),
                isSuggestion: reader.GetBool(EventTable.IsSuggestion)
            );

            DateTime? start = reader.GetNullableDateTime(TimedEventTable.Start);
            if (start == null) return @event;

            @event = TimedEvent.New(
                @event,
                (DateTime)start,
                end: reader.GetNullableDateTime(TimedEventTable.End)
            );

            double? price = reader.GetNullableDouble(PaidEventTable.Price);
            if (price == null) return @event;

            return PaidEvent.New(
                (TimedEvent)@event,
                (double)price,
                maxParticipants: reader.GetNullableInt32(PaidEventTable.MaxParticipants)
            );
        }
        private EventParticipance CreateEventParticipance(IDbDataReader reader)
        {
            try
            {
                return new EventParticipance(
                    id: reader.GetId(EventParticipanceTable.Id),
                    userId: reader.GetId(EventParticipanceTable.UserId),
                    eventId: reader.GetId(EventParticipanceTable.EventId),
                    participance: reader.GetString(EventParticipanceTable.State) switch
                    {
                        "none" => EventParticipanceEnum.None,
                        "signed" => EventParticipanceEnum.Signed,
                        "present" => EventParticipanceEnum.Present,
                        _ => throw DeveloperExceptions.MissingEnumValue(typeof(EventParticipanceEnum))
                    }
                );
            }
            catch
            {
                throw DeveloperExceptions.IncorrectData<EventParticipance>(reader.GetString(EventParticipanceTable.Id));
            }
        }
        private PushNotificationSubscription CreatePushNotificationSubscription(IDbDataReader reader)
        {
            return new PushNotificationSubscription(
                userId: reader.GetId(PushNotificationSubscriptionTable.UserId),
                auth: reader.GetString(PushNotificationSubscriptionTable.Auth),
                endpoint: reader.GetString(PushNotificationSubscriptionTable.Endpoint),
                p256DH: reader.GetString(PushNotificationSubscriptionTable.P256DH)
            );
        }
        private Credentials CreateCredentials(IDbDataReader reader)
        {
            return new Credentials(
                userId: reader.GetId(CredentialsTable.Id),
                email: reader.GetString(CredentialsTable.Email),
                passwordHash: reader.GetString(CredentialsTable.PasswordHash),
                salt: reader.GetString(CredentialsTable.Salt)
            );
        }
    }
}
