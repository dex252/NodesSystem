using System.Text;
using Dapper;
using Nancy;
using Newtonsoft.Json;
using SatanaServer.Helper;

namespace SatanaServer.Response.UnitsResponse
{
    class GetResponce : ResponseMethod
    {
        public GetResponce(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            int nodeId = request.Query["nodeId"];

            var unit = db.Db.Get<Unit>(nodeId);
           
            var myJson = JsonConvert.SerializeObject(unit);
            var jsonBytes = Encoding.UTF8.GetBytes(myJson);

            return Resp.SendResponse(jsonBytes);
        }
    }
}
