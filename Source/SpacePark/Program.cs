using SpacePark.Networking;
using System;

namespace SpacePark
{
    class Program
    {
        static void Main(string[] args)
        {
            StarWarsAPI api = new StarWarsAPI();
            api.UserFromStarWars("");
        }
    }
}
