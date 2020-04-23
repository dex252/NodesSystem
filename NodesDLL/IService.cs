using System.Collections.Generic;
using NodesDLL.Services;

namespace NodesDLL
{
    public interface IService
    {
        string path { get; }
        Bonds<List<Node>> Bonds { get; }
        Units Units { get; }
    }

    public class Service : IService
    {
        public string path => "http://127.0.0.1:1235";
        public Bonds<List<Node>> Bonds => new Bonds<List<Node>>(path);
        public Units Units => new Units(path);
    }
}
