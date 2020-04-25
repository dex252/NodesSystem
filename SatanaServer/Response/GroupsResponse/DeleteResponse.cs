using System.Data;
using System.Linq;
using Dapper;
using Nancy;
using SatanaServer.Helper;

namespace SatanaServer.Response.GroupsResponse
{
    class DeleteResponse : ResponseMethod
    {
        public DeleteResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            int groupId = request.Query["groupId"];

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                var sqlList = "select id from bonds where groupId=@groupId";
                var listId = connection.Query<int>(sqlList, new {groupId}, transaction).ToList();

                var sqlUnits = "delete from units where id=@id";

                foreach (var id in listId)
                {
                    connection.Query(sqlUnits, new {id}, transaction);
                }

                var sqlBonds = "delete from bonds where groupId=@groupId";
                connection.Query(sqlBonds, new{groupId}, transaction);

                var sqlGroups = "delete from groupsnodes where id=@groupId";
                connection.Query(sqlGroups, new { groupId }, transaction);

                transaction.Commit();
            }

            return Resp.SendResponse();
        }
    }
}
