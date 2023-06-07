using Infrastructure.Tables.Interfaces;
using MySql.Data.MySqlClient;
using Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.SQL
{
    public class Delete: Base
    {
        public Delete(MySqlCommand command, ITable table) : base(command, table, CRUD.DELETE)
        {
            command.CommandText = $"DELETE FROM {table.TableName}";
        }
        public Delete(Base prev) : base(prev) { }
        public void Prepare()
        {
            if (!command.CommandText.Contains("WHERE")) throw new DeveloperException("Cannot have a Delete statement without a Where caluse");
        }
        public Where Where(string column)
        {
            AddText("WHERE");
            return new Where(column, this);
        }
    }
}
