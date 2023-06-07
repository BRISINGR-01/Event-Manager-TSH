using Infrastructure.Tables.Interfaces;
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
    public class Where: Base
    {
        readonly string column;
        public Where(string column, Base prev): base(prev)
        {
            this.column = column;
        }
        public AndOr And
        {
            get
            {
                AddText("AND");
                return new AndOr(this);
            }
        }
        public AndOr Or
        {
            get
            {
                AddText("OR");
                return new AndOr(this);
            }
        }

        public new Where Equals(object? val)
        {
            AddText($"{column} = @{GetParam(ParseValue(val))}");

            return this;
        }
        public Where Equals<T>(Enum val) where T : ITableWithEnums, new()
        {
            string value;
            try
            {
                value = new T().EnumToSQLValue(val);
            }
            catch
            {
                throw DeveloperExceptions.InvalidEnum(val);
            }

            AddText($"{column} = @{GetParam(value)}");
            return this;
        }
        public Where Contains(string val)
        {
            AddText($"{column} = @{GetParam(val)}");
            return this;
        }
        public Where IsLess(string val) 
        {
            AddText($"{column} < @{GetParam(val)}");
            return this; 
        }
        public Where IsLessOrEqual(DateTime val)
        {
            AddText($"{column} < @{GetParam(Helpers.FormatDate(val))}");

            return this;
        }
        public Where IsMore(string val) 
        {   
            AddText($"{column} > @{GetParam(val)}");

            return this; 
        }
        public Where IsMoreOrEqual(DateTime val)
        {
            AddText($"{column} > @{GetParam(Helpers.FormatDate(val))}");

            return this;
        }
        public Select OrderBy(string column, bool isAscending = true)
        {
            if (type != CRUD.READ) throw new DeveloperException("You can order only a select statement!");

            AddText($"ORDER BY {column} {(isAscending ? "ASC" : "DESC")}");

            return FinishSelect;
        }
        public Select FinishSelect { get => new(this); }
    }
}
