using SpacePark.DB.Models;
using SpacePark.Networking;
namespace SpacePark.Logic
{
    public class Logic
    {
        public bool EndParkingByName(string name)
        {
            var res = new ParkingStatus().GetByCusomterName(name);
            if (res == null)
                return false;


            Customer.GetByID(res.CustomerID).Delete();
            return true;
        }

        public bool CanUserPark(string name)
        {

            var api = new StarWarsAPI();

            if (!api.UserFromStarWars(name))
                return false;

            if (Customer.GetByName(name) != null)
                return false;

            return true;
        }
    }
}