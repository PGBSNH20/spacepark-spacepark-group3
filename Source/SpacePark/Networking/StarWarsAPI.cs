using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using SpacePark.Config;
using SpacePark.DB.Models;

namespace SpacePark.Networking
{
    public class StarWarsAPI
    {
        private readonly RestClient _client;

        public StarWarsAPI()
        {
            _client = new RestClient(AppConfig.GetConfig().APIUrl);
        }

        public JObject GetJsonData(string endpoint)
        {
            RestRequest request = new(endpoint, Method.GET);
            request.OnBeforeDeserialization = resp => resp.ContentType = "application/json";

            var result = _client.Execute(request);
            return JObject.Parse(result.Content);
        }

        public bool UserFromStarWars(string name)
        {
            var jsonObject = GetJsonData("/people/");

            List<string> names = new();
            foreach (var entry in jsonObject["results"])
            {
                names.Add(entry["name"].ToString());
            }

            return names.Any(n => string.Equals(n, name, System.StringComparison.OrdinalIgnoreCase));
        }

        public List<Ship> GetStarWarsShips()
        {
            List<Ship> ships = new();
            var jsonObject = GetJsonData("/starships/");

            foreach (var entry in jsonObject["results"])
            {
                Ship ship = new();
                ship.Name = entry["name"].ToString();
                string length = entry["length"].ToString();
                length = length.Replace(",", ""); // Decimal/dot structure in API is inconsistent
                int index = length.IndexOf(".");
                if (index > 0)
                    length = length.Substring(0, index);
                ship.Length = int.Parse(length);
                ships.Add(ship);
            }

            return ships;
        }
    }
}
