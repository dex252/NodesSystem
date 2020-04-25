using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NodesDLL.Models;
using RestSharp;

namespace NodesDLL.Services
{
    public class Groups
    {
        private string path;
        private string resources = "groups";
        public Groups(string path)
        {
            this.path = path;
        }

        public List<Models.Groups> Get()
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

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            var groups = JsonConvert.DeserializeObject<List<Models.Groups>>(response.Content);
            return groups;
        }

        public bool Create(Node node, Models.Groups group)
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
            request.AddJsonBody(new Tree(node, group));

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int groupId)
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
                Method = Method.DELETE,
                RequestFormat = DataFormat.Json
            };
            request.Resource += "?groupId=" + groupId;
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return false;
            }

            return true;
        }
    }
}
