using System;
using SpacePark.DB.Models;
using SpacePark.Models;
using SpacePark.Networking;
namespace SpacePark.Logic
{
    public class Logic
    {
        public Invoice EndParkingByName(string name)
        {
            var res = ParkingStatus.GetByCustomerName(name);
            if (res == null)
                return null;

            Customer.GetByID(res.CustomerID).Delete();
            Spot spot = Spot.GetByID(res.SpotID);
            return new Invoice(res.ArrivalTime, DateTime.Now, spot.Price);
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

        public void CreateParkingStatus(string name, int spotID)
        {
            var userID = new Customer(name).Create();
            var data = new ParkingStatus(DateTime.Now, userID, spotID);

            data.Create();
        }

        public ParkingStatus GetParkingStatusBySpotID(int spotID)
        {
            var status = ParkingStatus.GetBySpotID(spotID);
            status.Customer = Customer.GetByID(status.CustomerID);
            status.Spot = Spot.GetByID(status.SpotID);

            return status;
        }
    }
}