using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace NodesDLL.Services
{
    public class Units
    {
        private string path;
        private string resources = "units";
        private string resourcesFullName = "units/fullname";
        public Units(string path)
        {
            this.path = path;
        }

        public Dictionary<int?, string> GetNames(List<int?> items)
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
            request.AddJsonBody(items);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            var dictionary = JsonConvert.DeserializeObject<Dictionary<int?, string>>(response.Content);
            return dictionary;
        }

        public Unit Get(int? nodeId)
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

            request.Resource += "?nodeId=" + nodeId;

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            var unit = JsonConvert.DeserializeObject<Unit>(response.Content);
            return unit;
        }

        public Dictionary<int?, string> GetFullNames(List<int?> items)
        {
            RestClient client = new RestClient(path)
            {
                Encoding = new UTF8Encoding(false)
            };

            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
            client.AddDefaultHeader("Content-Type", "application/json");

            var request = new RestSharp.RestRequest(resourcesFullName)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(items);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            var dictionary = JsonConvert.DeserializeObject<Dictionary<int?, string>>(response.Content);
            return dictionary;
        }
    }
}
