using Nancy;

namespace SatanaServer
{
    public static class Extensions
    {
        public static Database GetDb(this NancyContext context)
        {
            if (context != null && context.Items.ContainsKey("db"))
            {
                return (Database)context.Items["db"];
            }

            return null;
        }
    }
}
