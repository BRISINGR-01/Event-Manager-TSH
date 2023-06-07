using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Errors
{
    public class DatabaseActionException: Exception
    {
        public DatabaseActionException(string? area, CRUD action) : base(area == null ? "A problem occurred" : $"{CRUDToString(action)} the {area}") { }

        private static string CRUDToString(CRUD action) => action switch
        {
            CRUD.CREATE => "A problem occurred while creating",
            CRUD.READ => "A problem occurred while getting",
            CRUD.UPDATE => "A problem occurred while updating",
            CRUD.DELETE => "A problem occurred while deleting",
            _ => "There was a problem with"
        };
    }

    public enum CRUD
    {
        CREATE,
        READ,
        UPDATE,
        DELETE,
    }
}
