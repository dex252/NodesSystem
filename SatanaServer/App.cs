using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using NLog;

namespace SatanaServer
{
    class App
    {
        public static readonly Logger log = LogManager.GetCurrentClassLogger();
        static void Main()
        {
            string url = ConfigurationManager.ConnectionStrings["Host"].ConnectionString;

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Running on {url}");
                Console.ReadKey();
            }
        }
    }
}
