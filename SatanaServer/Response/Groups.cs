﻿using System;
using Nancy;
using SatanaServer.Response.GroupsResponse;

namespace SatanaServer.Response
{
    class Groups : Response
    {
        public override Nancy.Response Post(Request request, Database db)=>new PostResponse(db).Method(request);

        public override Nancy.Response Put(Request request, Database db)=>new PutResponse(db).Method(request);

        public override Nancy.Response Get(Request request, Database db) => new GetResponse(db).Method(request);

        public override Nancy.Response Delete(Request request, Database db)=>new DeleteResponse(db).Method(request);
    }
}
