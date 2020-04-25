using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Nancy;
using SatanaServer.Helper;

namespace SatanaServer.Response.NodesResponse
{
    class DeleteResponse : ResponseMethod
    {
        public DeleteResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            int nodeId = request.Query["nodeId"];

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                var idList = new List<int>();
                idList.Add(nodeId);

                var list = connection.GetList<Models.Bonds>().ToList();
                StartSelect(ref idList, list, nodeId);

                foreach (var id in idList)
                {
                    var bonds = "delete from bonds where id=@id";
                    var units = "delete from units where id=@id";

                    connection.Query(bonds, new {id}, transaction);
                    connection.Query(units, new {id}, transaction);
                }

                transaction.Commit();
            }
            return Resp.SendResponse();
        }

        private void StartSelect(ref List<int> idList, List<Models.Bonds> _list, int nodeId)
        {
            var list = _list.Where(e => e.parentId == nodeId);

            foreach (var e in list)
            {
                if (e.id == null) continue;
                idList.Add((int) e.id);
                StartSelect(ref idList, _list, (int)e.id);
            }
        }
    }
}
