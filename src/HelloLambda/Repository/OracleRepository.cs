using System;
using Devart.Data.Oracle;
using System.Data;
using Dapper;

namespace HelloLambda.Repository
{
    public class OracleRepository : IOracleRepository
    {
        private static IDbConnection _connection;
        public OracleRepository(Settings settings)
        {
            _connection = new OracleConnection(settings.OracleConnectionString);
        }

        public void InsertRecord(string message)
        {
            _connection.Execute(InsertRecordCommand, new { message });
        }

        public void UpdateRecord(int id, string message)
        {
            _connection.Execute(UpdateRecordCommand, new {id, message});
        }


        private const string UpdateRecordCommand = @"update VAN_WORKSHOP
                                                       set WORKSHOPMESSAGE = :message
                                                     where
                                                       WORKSHOPID = :id";

        private const string InsertRecordCommand = @"insert into VAN_WORKSHOP 
                                                       (WORKSHOPMESSAGE)
                                                     values
                                                       (:message)";

    }
}
