﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Nancy;
using Newtonsoft.Json;
using SatanaServer.Helper;

namespace SatanaServer.Response.BondsResponse
{
    class GetResponse : ResponseMethod
    {
        public GetResponse(Database db) : base(db)
        {
        }

        public override Nancy.Response Method(Request request)
        {
            int groupId = request.Query["groupId"];

            string sql = "SELECT * FROM Bonds WHERE groupId=@groupId";
            List<Node> bonds = db.Db.Query<Node>(sql, new{ groupId }).ToList();

            var myJson = JsonConvert.SerializeObject(bonds);
            var jsonBytes = Encoding.UTF8.GetBytes(myJson);

            return Resp.SendResponse(jsonBytes);
        }
    }
}
