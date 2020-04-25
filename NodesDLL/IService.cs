using System.Collections.Generic;
using NodesDLL.Services;

namespace NodesDLL
{
    public interface IService
    {
        string path { get; }
        Bonds<List<Node>> Bonds { get; }
        Units Units { get; }
        Services.Types Types { get; }
        Nodes Nodes { get; }
        Services.Groups Groups { get; }
    }

    public class Service : IService
    {
        public string path => "http://127.0.0.1:1235";
        public Bonds<List<Node>> Bonds => new Bonds<List<Node>>(path);
        public Units Units => new Units(path);
        public Services.Types Types => new Services.Types(path);
        public Nodes Nodes => new Nodes(path);
        public Services.Groups Groups => new Services.Groups(path);
    }
}
