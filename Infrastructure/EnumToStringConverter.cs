using Shared.Enums;
using SQL_Query_Builder.Interfaces;

namespace Infrastructure
{
    public class EnumToStringConverter : IEnumToStringConverter
    {
        public string Convert(Enum @enum)
        {
            if (@enum is UserRole role)
            {
                switch (role)
                {
                    case UserRole.EventOrganizer:
                        return "event_organizer";
                    case UserRole.StudentComitee:
                        return "student_comitee";
                }
            }

            return @enum.ToString();
        }
    }
}
