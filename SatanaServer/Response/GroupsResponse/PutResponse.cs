using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Nancy;
using Nancy.Extensions;
using SatanaServer.Helper;

namespace SatanaServer.Response.GroupsResponse
{
    class PutResponse : ResponseMethod
    {
        public PutResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            var body = request.Body.AsString();

            var tree = Newtonsoft.Json.JsonConvert.DeserializeObject<Tree>(body);

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                int? id = connection.Insert(tree.group, transaction);

                Move(tree.node, connection, transaction, null, id);
                
                transaction.Commit();
            }

            return Resp.SendResponse();
        }

        public void Move(Node node, MySqlConnection connection, MySqlTransaction transaction, int? newId, int? groupId)
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
