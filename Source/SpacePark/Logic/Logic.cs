using System;
using SpacePark.DB.Models;
using SpacePark.Networking;
namespace SpacePark.Logic
{
    public class Logic
    {
        public bool EndParkingByName(string name)
        {
            var res = ParkingStatus.GetByCusomterName(name);
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

        public void CreateParkingStatus(string name, int spotID) {
            var userID = new Customer(name).Create();
            var data = new ParkingStatus(DateTime.Now, userID, spotID);

            data.Create();
        }

        public ParkingStatus GetParkingStatusBySpotID(int spotID)
        {
            var status = ParkingStatus.GetBySpotID(4);
            status.Customer = Customer.GetByID(status.ID);
            status.Spot = Spot.GetByID(status.SpotID);

            return status;
        }
    }
}