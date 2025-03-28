using System.Data.Common;
using BStorm.Tools.CommandQuerySeparation.Commands;
using BStorm.Tools.Database;
using ICqsResult = BStorm.Tools.CommandQuerySeparation.Results.IResult;

namespace WriteApi.Models.Commands
{
    public class CreateMessageCommand : ICommandDefinition
    {
        public Guid Uid { get; }
        public DateTime Date { get; }
        public string Nom { get; }
        public string Content { get; }
        public CreateMessageCommand(string nom, string content)
        {
            Uid = Guid.NewGuid();
            Date = DateTime.Now.Date;
            Nom = nom;
            Content = content;
        }
    }

    public class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand>
    {
        private readonly DbConnection _dbConnection;

        public CreateMessageCommandHandler(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public ICqsResult Execute(CreateMessageCommand command)
        {
            try
            {
                _dbConnection.ExecuteNonQuery("CSP_AddMessage", true, command);
                return ICqsResult.Success();
            }
            catch (Exception ex)
            {
                return ICqsResult.Failure(ex.Message, ex);
            }
        }
    }
}
