using Nancy;

namespace SatanaServer.Response
{
    public abstract class ResponseMethod
    {
        public Database db;

        protected ResponseMethod(Database db)
        {
            this.db = db;
        }

        public abstract Nancy.Response Method(Request request);
    }
}
