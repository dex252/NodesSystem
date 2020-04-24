using System;
using Nancy;
using SatanaServer.Response.TypesResponse;

namespace SatanaServer.Response
{
    class Types : Response
    {
        public override Nancy.Response Post(Request request, Database db)
        {
            throw new NotImplementedException();
        }

        public override Nancy.Response Put(Request request, Database db)
        {
            throw new NotImplementedException();
        }

        public override Nancy.Response Get(Request request, Database db)=> new GetResponse(db).Method(request);

        public override Nancy.Response Delete(Request request, Database db)
        {
            throw new NotImplementedException();
        }
    }
}
