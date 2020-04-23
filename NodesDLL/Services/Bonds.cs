using System.Text;
using Newtonsoft.Json;
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

        public T Get()
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
                return default(T);
            }

            var bonds = JsonConvert.DeserializeObject<T>(response.Content);
            return bonds;
        }
    }
}
