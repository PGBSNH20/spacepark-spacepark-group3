using RestSharp;
using System;

namespace SpacePark.Networking
{
    class APIResponse
    {

    }
    public class StarWarsAPI
    {
        readonly string apiUrl = "https://swapi.dev/api/";
        RestClient client;

        public StarWarsAPI()
        {
            client = new RestClient(apiUrl);
        }

        public bool UserFromStarWars(string name)
        {
            RestRequest request = new RestRequest("people", Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var result = client.Execute(request);

            Console.WriteLine(result.Content);
            return false;
        }
    }
}
