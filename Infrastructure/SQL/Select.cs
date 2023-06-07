using Google.Protobuf.WellKnownTypes;
using Infrastructure.Tables.Interfaces;
using Logic.Interfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQL
{
    public class Select : Base
    {
        private string[]? selectedColumns;
        public Select(MySqlCommand command, ITable table) : base(command, table, CRUD.READ)
        {
            command.CommandText = $"SELECT";
        }
        public Select(Base prev) : base(prev) { }

        public Join Join(ITable table) => new(table, this);

        public Select All
        {
            get
            {
                AddText($"* FROM {table.TableName}");

                return this;
            }
        }
        public Select Count
        {
            get
            {
                AddText($"COUNT(*) FROM {table.TableName}");

                return this;
            }
        }

        public Select Only(params string[] columns)
        {
            selectedColumns = columns;
            AddText($"{string.Join(", ", columns)} FROM {table.TableName}");

            return this;
        }

        public Where Where(String column)
        {
            if (command.CommandText == "SELECT") return All.Where(column);

            AddText("WHERE");
            return new(column, this);
        }

        public T? First<T>() where T : IEntity
        {
            if (selectedColumns != null) throw new DeveloperException("You cannot use this method after calling Only()");

            T? value = default;

            command.Prepare();
            var reader = command.ExecuteReader();

            if (reader.Read()) value = EntityFromSQL<T>.Convert(reader);

            CloseConnection();

            return value;
        }

        public List<T> Get<T>() where T : IEntity
        {
            if (selectedColumns != null) throw new DeveloperException("You cannot use this method after calling Only()");
            List<T> values = new();

            command.Prepare();
            var reader = command.ExecuteReader();
            Exception? ex = null;
            while (reader.Read())
            {
                try
                {
                    values.Add(EntityFromSQL<T>.Convert(reader));
                } catch(Exception e)
                {
                    ex = e;
                }
            }

            if (values.Count == 0 && ex != null) throw ex;

            CloseConnection();

            return values;
        }

        public int CountValue
        {
            get
            {
                if (!command.CommandText.Contains("COUNT(")) throw new DeveloperException("You cannot use this method without Count");

                command.Prepare();
                var count = Convert.ToInt32(command.ExecuteScalar());

                CloseConnection();

                return count;
            }
        }

        public Dictionary<string, object> Value
        {
            get
            {
                if (selectedColumns == null) throw new DeveloperException("You cannot use this method without calling Only()");

                Dictionary<string, object> value = new();

                command.Prepare();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    foreach (var column in selectedColumns)
                    {
                        value.Add(column, reader.GetValue(reader.GetOrdinal(column)));
                    }
                }

                CloseConnection();

                return value;
            }
        }
        public Select OrderBy(string column, bool isAscending = true)
        {
            if (type != CRUD.READ) throw new DeveloperException("You can order only a select statement!");

            AddText($"ORDER BY {column} {(isAscending ? "ASC" : "DESC")}");

            return this;
        }
    }
}
