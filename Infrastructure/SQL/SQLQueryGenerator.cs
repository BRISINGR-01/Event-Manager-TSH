using MySql.Data.MySqlClient;
using Logic.Interfaces;
using Infrastructure.Tables;
using Logic.Models.Images;
using Logic;
using Infrastructure.Tables.Interfaces;
using Infrastructure.Tables.Events;
using Logic.Models.Events;
using Shared.Errors;

namespace Infrastructure.SQL
{
    public class SQLQueryGenerator<T> where T : ITable, new()
    {
        private readonly MySqlConnection connection;
        private ITable table;
        private MySqlCommand command { get { 
            MySqlCommand comm = new()
            {
                Connection = connection
            };
            try
            {
                comm.Connection.Open();
            }
            catch (MySqlException ex)
            {
                throw ex.Number switch
                {
                    0 => DeveloperExceptions.DatabaseConnectionException("Cannot connect to server"),
                    1042 => DeveloperExceptions.DatabaseConnectionException("Cannot connect to the database"),
                    1045 => DeveloperExceptions.DatabaseConnectionException("Invalid username/password provided"),
                    _ => DeveloperExceptions.DatabaseConnectionException("Something went wrong while connecting to the database: " + ex.Message),
                };
            };
            return comm;
        } }
        public SQLQueryGenerator(MySqlConnection connection)
        {
            this.connection = connection;
            this.table = new T();
        }

        public SQLQueryGenerator<NewT> From<NewT>() where NewT : ITable, new()
        {
            return new SQLQueryGenerator<NewT>(connection);
        }


        public Select Select { get => new(command, table); }
        public Insert Insert { get => new(command, table); }
        public Update Update { get => new(command, table); }
        public Delete Delete { get => new(command, table); }
    }
}
