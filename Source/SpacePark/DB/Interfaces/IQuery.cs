using SpacePark.DB.Models;
using System.Collections.Generic;

namespace SpacePark.DB.Interfaces
{
    public interface IQuery
    {
        IEnumerable<Spot> GetSpots();
        void CreateCustomer(Customer customer);
        Customer GetCustomer(string name);
        void CreateParkingStatus(ParkingStatus parkingStatus);
        IEnumerable<ParkingStatus> GetAllParkingStatus();
        ParkingStatus GetParkingStatusByCustomer(Customer customer);
        ParkingStatus GetParkingStatusByName(string name);
        ParkingStatus GetParkingStatusBySpotID(int id);
        void DeleteParkingStatus(ParkingStatus parkingStatus);
        void CreateShip(Ship ship);
        Ship GetShipByPlate(string plate);
    }
}