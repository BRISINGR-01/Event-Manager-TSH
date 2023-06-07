using Infrastructure.Tables;
using Infrastructure.Tables.Events;
using Logic;
using Logic.Interfaces;
using Logic.Models;
using Logic.Models.Events;
using MySql.Data.MySqlClient;
using Shared.Enums;
using Shared.Errors;

namespace Infrastructure.SQL
{
    public static class EntityFromSQL<Entity> where Entity : IEntity
    {
        public static Entity Convert(MySqlDataReader reader)
        {
            return typeof(Entity) switch
            {
                Type UserType when UserType == typeof(User) => (Entity)(IEntity)UserConvert(reader),
                Type BranchType when BranchType == typeof(Branch) => (Entity)(IEntity)BranchConvert(reader),
                Type EventType when EventType == typeof(Event) => (Entity)(IEntity)EventConvert(reader),
                Type EventParticipanceType when EventParticipanceType == typeof(EventParticipance) => (Entity)(IEntity)EventParticipanceConvert(reader),
                _ => throw new DeveloperException($"A converter for {typeof(Entity).Name} is not implemented yet"),
            };
        }
        private static User UserConvert(MySqlDataReader reader)
        {
            try
            {

                return new User(
                    id: Guid.Parse(reader.GetString(UserTable.Id)),
                    branchId: Guid.Parse(reader.GetString(UserTable.BranchId)),
                    userName: new Encryption().Decrypt(reader.GetString(UserTable.UserName)) ?? throw new Exception(),
                    password: new Encryption().Decrypt(reader.GetString(UserTable.Password)) ?? throw new Exception(),
                    role: reader.GetString(UserTable.Role) switch
                    {
                        "event_organizer" => UserRole.EventOrganizer,
                        "administrator" => UserRole.Administrator,
                        "student" => UserRole.Student,
                        "student_comitee" => UserRole.StudentComitee,
                        "guest" => UserRole.Guest,
                        _ => throw DeveloperExceptions.MissingEnumValue(typeof(UserRole))
                    },
                    email: new Encryption().Decrypt(reader.GetString(UserTable.Email)) ?? throw new Exception()
                );
            }
            catch
            {
                throw DeveloperExceptions.IncorrectData<User>(reader.GetString(UserTable.Id));
            }
        }
        private static Branch BranchConvert(MySqlDataReader reader)
        {
            try
            {
                return new Branch(
                    id: Guid.Parse(reader.GetString(BranchTable.Id)),
                    name: reader.GetString(BranchTable.Name)
                );
            }
            catch
            {
                throw DeveloperExceptions.IncorrectData<Branch>(reader.GetString(BranchTable.Id));
            }
        }
        private static Event EventConvert(MySqlDataReader reader)
        {
            try
            {
                Guid branchId = Guid.Parse(reader.GetString(EventTable.BranchId));
                Guid id = Guid.Parse(reader.GetString(EventTable.Id));
                DateTime? start = reader.IsDBNull(reader.GetOrdinal(TimedEventTable.Start)) ? null : reader.GetDateTime(TimedEventTable.Start);
                double? price = reader.IsDBNull(reader.GetOrdinal(PaidEventTable.Price)) ? null : reader.GetDouble(PaidEventTable.Price);

                if (start != null)
                {
                    if (price != null)
                    {
                        return new PaidEvent(
                            id,
                            branchId,
                            title: reader.GetString(EventTable.Title),
                            description: reader.GetString(EventTable.Description),
                            (DateTime)start,
                            end: reader.IsDBNull(reader.GetOrdinal(TimedEventTable.End)) ? null : reader.GetDateTime(TimedEventTable.End),
                            venue: reader.IsDBNull(reader.GetOrdinal(EventTable.Venue)) ? null : reader.GetString(EventTable.Venue),
                            (double)price,
                            maxParticipants: reader.IsDBNull(reader.GetOrdinal(PaidEventTable.MaxParticipants)) ? null : reader.GetInt32(PaidEventTable.MaxParticipants)
                        );
                    }
                    
                    return new TimedEvent(
                        id,
                        branchId,
                        title: reader.GetString(EventTable.Title),
                        description: reader.GetString(EventTable.Description),
                        (DateTime)start,
                        end: reader.IsDBNull(reader.GetOrdinal(TimedEventTable.End)) ? null : reader.GetDateTime(TimedEventTable.End),
                        venue: reader.IsDBNull(reader.GetOrdinal(EventTable.Venue)) ? null : reader.GetString(EventTable.Venue)
                    );
                }

                return new Event(
                    id,
                    branchId,
                    title: reader.GetString(EventTable.Title),
                    description: reader.GetString(EventTable.Description),
                    venue: reader.IsDBNull(reader.GetOrdinal(EventTable.Venue)) ? null : reader.GetString(EventTable.Venue)
                );
            }
            catch
            {
                throw DeveloperExceptions.IncorrectData<Event>(reader.GetString(EventTable.Id));
            }
        }
        private static EventParticipance EventParticipanceConvert(MySqlDataReader reader)
        {
            try
            {
                return new EventParticipance(
                    id: Guid.Parse(reader.GetString(EventParticipanceTable.Id)),
                    userId: Guid.Parse(reader.GetString(EventParticipanceTable.UserId)),
                    eventId: Guid.Parse(reader.GetString(EventParticipanceTable.EventId)),
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
                throw DeveloperExceptions.IncorrectData<Logic.Models.Images.Image>(reader.GetString(EventParticipanceTable.Id));
            }
        }
    }
}
