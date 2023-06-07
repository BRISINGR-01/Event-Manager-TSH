using Infrastructure.Tables.Interfaces;

namespace Infrastructure.SQL
{
    public class Join: Base
    {
        private readonly ITable table2;
        public Join(ITable table, Base prev): base(prev)
        {
            table2 = table;
            AddText($"LEFT JOIN {table2.TableName}");
        }

        public Select OnColumns(string column1, string column2)
        {
            AddText($"ON {table.TableName}.{column1} = {table2.TableName}.{column2}");
            return new Select(this);
        }
    }
}
