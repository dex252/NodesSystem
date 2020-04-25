using System.Data;
using System.Text;
using Dapper;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using SatanaServer.Helper;

namespace SatanaServer.Response.NodesResponse
{
    class PostResponse : ResponseMethod
    {
        public PostResponse(Database db) : base(db)
        {
        }
        public override Nancy.Response Method(Request request)
        {
            var body = request.Body.AsString();

            var node = Newtonsoft.Json.JsonConvert.DeserializeObject<Node>(body);

            var bonds = new Models.Bonds()
            {
                parentId = node.parentId,
                groupId = node.groupId
            };

            int? id = null;

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                var parent = connection.Get<Unit>(node.parentId);

                if (!string.IsNullOrEmpty(parent.name))
                {
                    id = connection.Insert(bonds, transaction);

                    if (id != null)
                    {
                        bonds.id = id;
                        bonds.nodeId = id;
                        connection.Update(bonds);

                        node.Unit.id = (int)id;
                        node.Unit.dependence = parent.name;
                        node.Unit.curatorial_position = parent.position;
                        node.Unit.curatorial_fullname = parent.fullname;
                        node.id = id;
                        node.nodeId = id;

                        var type = connection.Get<SatanaServer.Types>(node.Unit.type);
                        node.Unit.type = type.name;

                        connection.Insert(node.Unit, transaction);

                        transaction.Commit();
                    }
                   
                }
            }

            var myJson = JsonConvert.SerializeObject(id);
            var jsonBytes = Encoding.UTF8.GetBytes(myJson);

            return Resp.SendResponse(HttpStatusCode.OK, jsonBytes);
        }
    }
}
