using System.Data;
using Dapper;
using Nancy;
using Nancy.Extensions;
using SatanaServer.Helper;

namespace SatanaServer.Response.GroupsResponse
{
    class PostResponse : ResponseMethod
    {
        public PostResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            var body = request.Body.AsString();

            var tree = Newtonsoft.Json.JsonConvert.DeserializeObject<Tree>(body);

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                int? id = connection.Insert(tree.group, transaction);
                tree.node.Unit.id = id;

                var type = connection.Get<SatanaServer.Types>(tree.node.Unit.type, transaction);
                tree.node.Unit.type = type.name;

                int? nodeId = connection.Insert(new Models.Bonds()
                {
                    groupId = id,
                    parentId = null
                }, transaction);

                connection.Update(new Models.Bonds()
                {
                    groupId = id,
                    parentId = null,
                    nodeId = nodeId,
                    id = nodeId
                },transaction);

                tree.node.Unit.id = nodeId;

                connection.Insert(tree.node.Unit, transaction);

                transaction.Commit();
            }

            return Resp.SendResponse();
        }
    }
}
