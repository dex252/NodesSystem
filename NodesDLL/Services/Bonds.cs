using System;
using System.Text;
using Newtonsoft.Json;
using NodesDLL.Models;
using RestSharp;

namespace NodesDLL.Services
{
    public class Bonds<T>
    {
        private string path;
        private string resources = "bonds";
        public Bonds(string path)
        {
            this.path = path;
        }

        public T Get(int? groupId)
        {
            RestClient client = new RestClient(path)
            {
                Encoding = new UTF8Encoding(false)
            };

            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
            client.AddDefaultHeader("Content-Type", "application/json");

            var request = new RestSharp.RestRequest(resources)
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            request.Resource += "?groupId=" + groupId;
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return default(T);
            }

            var bonds = JsonConvert.DeserializeObject<T>(response.Content);
            return bonds;
        }

        public bool ConnectionNode(Node connectionNode, int? nodeIdInGroup)
        {
            RestClient client = new RestClient(path)
            {
                Encoding = new UTF8Encoding(false)
            };

            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
            client.AddDefaultHeader("Content-Type", "application/json");

            var request = new RestSharp.RestRequest(resources)
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new Tree(connectionNode, new Models.Groups()
            {
                id = nodeIdInGroup
            }));

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return false;
            }

            return true;
        }
    }
}
