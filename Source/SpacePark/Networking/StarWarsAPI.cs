using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace SpacePark.Networking
{
    public class StarWarsAPI
    {
        readonly string apiUrl = "https://swapi.dev/api";
        private readonly RestClient client;

        public StarWarsAPI()
        {
            client = new RestClient(apiUrl);
        }

        public bool UserFromStarWars(string name)
        {
            RestRequest request = new("/people/", Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var result = client.Execute(request);
            var jsonObject = JObject.Parse(result.Content);

            List<string> names = new();
            foreach (var entry in jsonObject["results"])
            {
                names.Add(entry["name"].ToString());
            }

            return names.Any(n => n.ToLower() == name.ToLower());
        }
    }
}
