using System;
using Nancy;

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

            throw new NotImplementedException();
        }
    }
}
