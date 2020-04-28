using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy;
using Nancy.Extensions;
using SatanaServer.Helper;

namespace SatanaServer.Response.BondsResponse
{
    class PutResponse : ResponseMethod
    {
        public PutResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            var body = request.Body.AsString();

            // Здесь нода - присоединяемая нода, в группе - id ноды к которой присоединяем дерево
            var tree = Newtonsoft.Json.JsonConvert.DeserializeObject<Tree>(body);

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                int? connectionId = tree.group.id; // присоединяемая нода
                Node node = tree.node; // нода, к которой коннектимся

                //получим id группы к которой присоединяем дерево
                var connNode = connection.Get<Models.Bonds>(connectionId, transaction);
                var newGroup = connNode.groupId;
                var newParentId = connNode.nodeId;

               // var node = bondsList.First(e => e.parentId == null);
                //var nodeId = node.id;
                Move(tree.node, connection, transaction, newParentId, newGroup);

                transaction.Commit();
            }

            return Resp.SendResponse();
        }

        private void Move(Node node, MySqlConnection connection, MySqlTransaction transaction, int? newId, int? groupId)
        {
            node.parentId = newId;

            int? nodeId = connection.Insert(new Models.Bonds()
            {
                groupId = groupId,
                parentId = node.parentId
            }, transaction);

            connection.Update(new Models.Bonds()
            {
                groupId = groupId,
                parentId = node.parentId,
                nodeId = nodeId,
                id = nodeId
            }, transaction);

            node.Unit.id = nodeId;
            connection.Insert(node.Unit, transaction);

            foreach (var n in node.nodes)
            {
                Move(n, connection, transaction, nodeId, groupId);
            }
        }
    }
}
