using System.Text;
using Dapper;
using Nancy;
using Newtonsoft.Json;
using SatanaServer.Helper;

namespace SatanaServer.Response.GroupsResponse
{
    class GetResponse : ResponseMethod
    {
        public GetResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            var groupList = db.Db.GetList<Models.Groups>();

            var myJson = JsonConvert.SerializeObject(groupList);
            var jsonBytes = Encoding.UTF8.GetBytes(myJson);

            return Resp.SendResponse(jsonBytes);
        }
    }
}
