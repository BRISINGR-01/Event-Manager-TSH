using SQL_Query_Builder;

namespace Infrastructure
{
    public abstract class DatabaseRepository
    {
        protected readonly SQLQueryBuilder sql;
        public DatabaseRepository(DatabaseManager db, string tableName)
        {
            sql = db.OfTable(tableName);
        }
    }
}
