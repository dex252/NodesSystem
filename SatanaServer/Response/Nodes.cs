﻿using System;
using Nancy;
using SatanaServer.Response.NodesResponse;

namespace SatanaServer.Response
{
    class Nodes : Response
    {
        public override Nancy.Response Post(Request request, Database db) => new PostResponse(db).Method(request);

        public override Nancy.Response Put(Request request, Database db) => new PutResponse(db).Method(request);

        public override Nancy.Response Get(Request request, Database db)
        {
            throw new NotImplementedException();
        }

        public override Nancy.Response Delete(Request request, Database db)=> new DeleteResponse(db).Method(request);
    }
}
