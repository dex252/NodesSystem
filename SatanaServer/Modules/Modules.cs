﻿using SatanaServer.Module;
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
    public class UnitsFullNameModule : MainModule
    {
        public UnitsFullNameModule() : base("units/fullname", new UnitsFullName())
        {
        }
    }
    public class UnitsPositionModule : MainModule
    {
        public UnitsPositionModule() : base("units/position", new UnitsPosition())
        {
        }
    }
    public class TypesModule : MainModule
    {
        public TypesModule() : base("types", new Response.Types())
        {
        }
    }
    public class NodesModule : MainModule
    {
        public NodesModule() : base("nodes", new Nodes())
        {
        }
    }

    public class GroupsModule : MainModule
    {
        public GroupsModule() : base("groups", new Groups())
        {
        }
    }
}
