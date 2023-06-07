using MySql.Data.MySqlClient;
using Logic;
using Shared.Enums;
using Shared;
using Shared.Errors;
using Infrastructure.Tables.Interfaces;

namespace Infrastructure.Tables
{
    public class UserTable: ITableWithEnums
    {
        public string TableName { get => "tsh_user"; }

        public static readonly string Id = "id";
        public static readonly string BranchId = "branch_id";
        public static readonly string UserName = "user_name";
        public static readonly string Password = "password";
        public static readonly string Role = "role";
        public static readonly string Email = "email";
        public static string EnumToSQLValue(Enum role)
        {
            return role switch
            {
                UserRole.EventOrganizer => "event_organizer",
                UserRole.Administrator => "administrator",
                UserRole.Student => "student",
                UserRole.StudentComitee => "student_comitee",
                UserRole.Guest => "guest",
                _ => throw DeveloperExceptions.InvalidEnum(role),
            };
        }
        string ITableWithEnums.EnumToSQLValue(Enum role)
        {
            return EnumToSQLValue(role);
        }
    }
}
