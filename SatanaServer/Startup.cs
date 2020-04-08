using Owin;

namespace SatanaServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseNancy();
        }
    }
}
