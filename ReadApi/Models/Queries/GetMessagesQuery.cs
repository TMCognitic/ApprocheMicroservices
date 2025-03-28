using System.Data.Common;
using BStorm.Tools.CommandQuerySeparation.Queries;
using BStorm.Tools.CommandQuerySeparation.Results;
using BStorm.Tools.Database;
using ReadApi.Models.Entities;

namespace ReadApi.Models.Queries
{
    public class GetMessagesQuery : IQueryDefinition<IEnumerable<Message>>
    {
    }

    public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, IEnumerable<Message>>
    {
        private readonly DbConnection _dbConnection;

        public GetMessagesQueryHandler(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }
        public IResult<IEnumerable<Message>> Execute(GetMessagesQuery query)
        {
            try
            {
                return IResult<IEnumerable<Message>>.Success(_dbConnection.ExecuteReader("SELECT * FROM Message;", dr => new Message((Guid)dr["Uid"], (DateTime)dr["Date"], (string)dr["Nom"], (string)dr["Content"])).ToArray());
            }
            catch (Exception ex)
            {
                return IResult<IEnumerable<Message>>.Failure(ex.Message, ex);
            }
        }
    }
}
