using MySql.Data.MySqlClient;
using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class SQLQueryBuilder
    {
        private readonly MySqlConnection connection;
        private readonly string tableName;
        private readonly IEnumToStringConverter converter;
        private readonly IEntityFactory entityFactory;
        private MySqlCommand command => GetCommand(connection.ConnectionString);
        public SQLQueryBuilder(string connectionString, string tableName, IEnumToStringConverter converter, IEntityFactory entityBuilder)
        {
            connection = new(connectionString);
            this.tableName = tableName;
            this.converter = converter;
            this.entityFactory = entityBuilder;
        }
        private static MySqlCommand GetCommand(string connectionString)
        {
            MySqlCommand cmd = new(string.Empty, new(connectionString));

            try
            {
                cmd.Connection.Open();
            }
            catch (MySqlException ex)
            {
                throw ex.Number switch
                {
                    0 => SQLQueryBuilderException.DatabaseConnectionException("Cannot connect to server"),
                    1042 => SQLQueryBuilderException.DatabaseConnectionException("Cannot connect to the database"),
                    1045 => SQLQueryBuilderException.DatabaseConnectionException("Invalid username/password provided"),
                    _ => SQLQueryBuilderException.DatabaseConnectionException("Something went wrong while connecting to the database: " + ex.Message),
                };
            };

            return cmd;
        }
        public SQLQueryBuilder FromTable(string tableName)
        {
            return new SQLQueryBuilder(connection.ConnectionString, tableName, converter, entityFactory);
        }
        public void HealthCheck() => Cmd.HealthCheck();
        private Command Cmd => new(command, tableName, converter, entityFactory);
        public Select.Select Select => new(Cmd);
        public Insert Insert => new(Cmd);
        public Update Update => new(Cmd);
        public Delete Delete => new(Cmd);
    }
}
