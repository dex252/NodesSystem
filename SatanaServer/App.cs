using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace SatanaServer
{
    class App
    {
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
