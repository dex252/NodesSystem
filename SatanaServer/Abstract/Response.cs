using Nancy;

namespace SatanaServer.Response
{
    public abstract class Response : NancyModule
    {
        public abstract Nancy.Response Post(Request request, Database db);
        public abstract Nancy.Response Put(Request request, Database db);
        public abstract Nancy.Response Get(Request request, Database db);
        public abstract Nancy.Response Delete(Request request, Database db);
    }
}
