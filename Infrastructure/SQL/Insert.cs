using Infrastructure.Tables.Interfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.SQL
{
    public class Insert: Base
    {
        private readonly List<string> columns = new();

        private IEnumerable<object?> allColumns { get => table.GetType().GetFields().Select(f => f.GetValue(table)); }
        public Insert(MySqlCommand command, ITable table) : base(command, table, CRUD.CREATE)
        {
            command.CommandText = $"INSERT INTO {table.TableName} ({string.Join(", ", allColumns)}) VALUES (";
        }
        public Insert(Base prev): base(prev) { }
        public Insert Set(string column, object? value)
        {
            columns.Add(column);
            if (value is byte[] bytes)
            {
                SetParam(column, bytes);
            } else
            {
                SetParam(column, ParseValue(value));
            }
            AddText($"@{column},");
            return this;
        }
        public void Prepare()
        {
            command.CommandText = Regex.Replace(command.CommandText, @",$", ")");
        }
    }
}
