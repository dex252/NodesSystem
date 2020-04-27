using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using SatanaServer.Helper;

namespace SatanaServer.Response.UnitsPositionResponse
{
    class PostResponse : ResponseMethod
    {
        public PostResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            var body = request.Body.AsString();

            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int?>>(body);
            Dictionary<int?, string> dictionary = new Dictionary<int?, string>();

            using (var connection = db.Db)
            using (var transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                foreach (var t in items)
                {
                    Unit unit = connection.Get<Unit>(t, transaction);

                    dictionary.Add(t, unit.position);
                }
            }

            var myJson = JsonConvert.SerializeObject(dictionary);
            var jsonBytes = Encoding.UTF8.GetBytes(myJson);

            return Resp.SendResponse(HttpStatusCode.OK, jsonBytes);
        }
    }
}
