using System;
using Dapper;
using Nancy;
using Nancy.Extensions;
using SatanaServer.Helper;

namespace SatanaServer.Response.NodesResponse
{
    class PutResponse : ResponseMethod
    {
        public PutResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            var body = request.Body.AsString();

            var node = Newtonsoft.Json.JsonConvert.DeserializeObject<Node>(body);

            var unit = node.Unit;

            var type = db.Db.Get<SatanaServer.Types>(node.Unit.type);
            unit.type = type.name;

            var success = db.Db.Update(unit);

            if (success != 1) return Resp.SendResponse(HttpStatusCode.Conflict);

            return Resp.SendResponse();
        }
    }
}
