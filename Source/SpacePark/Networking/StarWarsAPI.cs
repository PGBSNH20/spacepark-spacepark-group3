using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using SpacePark.Config;

namespace SpacePark.Networking
{
    public class StarWarsAPI
    {
        private readonly RestClient _client;

        public StarWarsAPI()
        {
            _client = new RestClient(AppConfig.GetConfig().APIUrl);
        }

        public bool UserFromStarWars(string name)
        {
            RestRequest request = new("/people/", Method.GET);
            request.OnBeforeDeserialization = resp => resp.ContentType = "application/json";

            var result = _client.Execute(request);
            var jsonObject = JObject.Parse(result.Content);

            List<string> names = new();
            foreach (var entry in jsonObject["results"])
            {
                names.Add(entry["name"].ToString());
            }

            return names.Any(n => string.Equals(n, name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
