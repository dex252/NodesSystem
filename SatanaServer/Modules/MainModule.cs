using MySql.Data.MySqlClient;
using Nancy;
using Nancy.Responses;

namespace SatanaServer.Module
{
    public abstract class MainModule : NancyModule
    {
        protected MainModule(string pathToRequest, Response.Response response)
        {
            Post($"/{pathToRequest}", action =>
            {
                Database db = Context.GetDb();

                try
                {
                    return response.Post(Context.Request, db);
                }
                catch (MySqlException e)
                {
                    return SendMySqlException(e);
                }
            }, null, "POST");

            Get($"/{pathToRequest}", action =>
            {
                Database db = Context.GetDb();

                try
                {
                    return response.Get(Context.Request, db);
                }
                catch (MySqlException e)
                {
                    return SendMySqlException(e);
                }
            }, null, "Get");

            Put($"/{pathToRequest}", action =>
            {
                Database db = Context.GetDb();

                try
                {
                    return response.Put(Context.Request, db);
                }
                catch (MySqlException e)
                {
                    return SendMySqlException(e);
                }
            }, null, "Put");

            Delete($"/{pathToRequest}", action =>
            {
                Database db = Context.GetDb();

                try
                {
                    return response.Delete(Context.Request, db);
                }
                catch (MySqlException e)
                {
                    return SendMySqlException(e);
                }
            }, null, "Delete");
        }

        private Nancy.Response SendMySqlException(MySqlException e)
        {
            App.log.Error($"{e.Message}\r\n{e.StackTrace}");
            switch (e.Number)
            {
                case 1062:
                {
                    string result = e.Message.Replace("Duplicate entry '", "");
                    result = result.Replace("' for key 'uq'", "");

                    return new TextResponse(HttpStatusCode.Conflict, $"Запись {result} уже существует");
                }

                default:
                {
                        //return Resp.SendResponse(HttpStatusCode.BadRequest);
                        return new TextResponse(HttpStatusCode.Conflict, e.Message);
                    }
            }
        }
    }


}
