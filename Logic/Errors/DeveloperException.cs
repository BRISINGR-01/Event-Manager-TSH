using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Errors
{
    public static class DeveloperExceptions
    {
        public static DeveloperException InvalidEnum(Enum @enum) => new($"The table does not contain a {@enum.GetType().Name} enum");
        public static DeveloperException ITableWithEnum => new("Table must implement ITableWithEnums if there are any enums");
        public static DeveloperException MissingEnumValue(Type @enum) => new($"The value of the enum {@enum.Name} was not correct");
        public static DeveloperException IncorrectData<T>(string id) => new($"Encountered incorrect data while trying to parse a {typeof(T).Name} entity with an id of {id}");
        public static DeveloperException MissingWhereException(string queryType) => new($"Cannot have a {queryType} statement without a WHERE clause (condition) {(queryType == "JOIN" ? "in the `ON` part - add a .AddJoinCondition() " : "add a .AddCondition")}"); 
        public static DeveloperException CannotFindDatabaseCredentials => new("Cannot find database credentials");
        public static ServerException DatabaseConnectionException(string message) => new(message);
        public static DeveloperException SQLException(string message) => new(message);
    }

    public class DeveloperException : Exception 
    { 
        public DeveloperException(string message) : base(message) { } 
    }
    public class ServerException: DeveloperException { public ServerException(string message) : base(message) { } }
}
