using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace NodesDLL.Services
{
    public class Types
    {
        private string path;
        private string resources = "types";
        public Types(string path)
        {
            this.path = path;
        }

        public List<NodesDLL.Types> Get()
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

            var types = JsonConvert.DeserializeObject<List<NodesDLL.Types>>(response.Content);
            return types;
        }
    }
}
