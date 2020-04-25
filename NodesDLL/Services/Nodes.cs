using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace NodesDLL.Services
{
    public class Nodes
    {
        private string path;
        private string resources = "nodes";
        public Nodes(string path)
        {
            this.path = path;
        }

        public int? Create(INode node)
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
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(node);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            var id = JsonConvert.DeserializeObject<int?>(response.Content);
            return id;
        }

        public bool Update(INode node)
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
            request.AddJsonBody(node);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return false;
            }

            return true;
        }
    }
}
