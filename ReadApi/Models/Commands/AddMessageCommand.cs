using System.Data;
using System.Data.Common;
using BStorm.Tools.CommandQuerySeparation.Commands;
using BStorm.Tools.Database;
using ICqsResult = BStorm.Tools.CommandQuerySeparation.Results.IResult;

namespace ReadApi.Models.Commands
{
    public class AddMessageCommand : ICommandDefinition
    {
        public Guid Uid { get; }
        public DateTime Date { get; }
        public string Nom { get; }
        public string Content { get; }
        public AddMessageCommand(Guid uid, DateTime date, string nom, string content)
        {
            Uid = uid;
            Date = date;
            Nom = nom;
            Content = content;
        }
    }

    public class AddMessageCommandHandler : ICommandHandler<AddMessageCommand>
    {
        private readonly DbConnection _dbConnection;

        public AddMessageCommandHandler(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ICqsResult Execute(AddMessageCommand command)
        {
            try
            {
                _dbConnection.Open();
                _dbConnection.ExecuteNonQuery("INSERT INTO Message (Uid, Date, Nom, Content) values (@Uid, @Date, @Nom, @Content)", false, command);
                return ICqsResult.Success();
            }
            catch (Exception ex)
            {
                return ICqsResult.Failure(ex.Message, ex);
            }
            finally
            {
                if( _dbConnection.State is ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }
    }
}
