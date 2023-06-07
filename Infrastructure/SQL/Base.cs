using Infrastructure.Tables.Interfaces;
using Logic.Interfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Shared;
using Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQL
{
    public class Base
    {
        public MySqlCommand command;
        public CRUD type;
        public ITable table;

        public Base(MySqlCommand command, ITable table, CRUD crud)
        {
            type = crud;
            this.table = table;
            this.command = command;
        }
        public Base(Base parent)
        {
            command = parent.command;
            table = parent.table;
            type = parent.type;
        }

        protected void AddText(string text)
        {
            command.CommandText += $" {text}";
        }

        protected void CloseConnection()
        {
            command.Connection.Close();
            command.Connection.Dispose();
        }
        protected string GetParam(string? value)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            string paramName = new(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
            SetParam($"@{paramName}", value);

            return paramName;
        }
        protected void SetParam(string param, string? value)
        {
            command.Parameters.AddWithValue($"@{param}", value);
        }
        protected void SetParam(string param, byte[] value)
        {
            command.Parameters.AddWithValue($"@{param}", value);
        }
        protected string? ParseValue(object? val)
        {
            string? value = null;
            if (val is Guid guid)
            {
                if (guid == Guid.Empty) throw new DeveloperException("Id is empty");

                return guid.ToString();
            }
            else if (val is DateTime date)
            {
                value = Helpers.FormatDate(date);
            } else if (val is Enum @enum)
            {
                if (table is not ITableWithEnums) throw DeveloperExceptions.ITableWithEnum;

                value = ((ITableWithEnums)table).EnumToSQLValue(@enum);
            }
            else if (val is not string && val != null)
            {
                value = val.ToString()!;
            }
            else if (val is string @str)
            {
                value = @str;
            }

            return value;
        }
        public bool Execute()
        {
            switch (type)
            {
                case CRUD.CREATE:
                    new Insert(this).Prepare();
                    break;
                case CRUD.UPDATE:
                    new Update(this).Prepare();
                    break;
            }

            bool result = false;
            try
            {
                command.Prepare();

                result = command.ExecuteNonQuery() > 0;
            }
            catch (MySqlException ex)
            {
                throw DeveloperExceptions.SQLException(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }
    }
}
