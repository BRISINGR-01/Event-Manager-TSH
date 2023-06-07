using Infrastructure.Tables.Interfaces;
using MySql.Data.MySqlClient;
using Shared.Errors;
using System.Text.RegularExpressions;

namespace Infrastructure.SQL
{
    public class Update: Base
    {
        public Update(MySqlCommand command, ITable table) : base(command, table, CRUD.UPDATE)
        {
            command.CommandText = $"UPDATE {table.TableName} SET";
        }
        public Update(Base prev) : base(prev) { }
        public Update Set(string column, object? val)
        {
            SetParam(column, ParseValue(val));
            
            AddText($"{column} = @{column},");
            return this;
        }
        public void Prepare()
        {
            if (!command.CommandText.Contains("WHERE")) throw new DeveloperException("Cannot have an Update statement without a Where caluse");
        }
        public Where Where(string column)
        {
            if (command.CommandText.EndsWith(",")) command.CommandText = Regex.Replace(command.CommandText, @",$", "");
            AddText("WHERE");
            return new Where(column, this);
        }
    }
}
