using SatanaServer.Module;
using SatanaServer.Response;

namespace SatanaServer.Modules
{
    public class BondsModule : MainModule
    {
        public BondsModule() : base("bonds", new Bonds())
        {
        }
    }
    public class UnitsModule : MainModule
    {
        public UnitsModule() : base("units", new Units())
        {
        }
    }
}
