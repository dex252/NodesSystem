using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Nancy;
using Newtonsoft.Json;
using SatanaServer.Helper;

namespace SatanaServer.Response.TypesResponse
{
    class GetResponse : ResponseMethod
    {
        public GetResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            List<SatanaServer.Types> types = db.Db.GetList<SatanaServer.Types>().ToList();

            var myJson = JsonConvert.SerializeObject(types);
            var jsonBytes = Encoding.UTF8.GetBytes(myJson);

            return Resp.SendResponse(jsonBytes);
        }
    }
}
