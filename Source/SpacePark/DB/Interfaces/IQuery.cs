using SpacePark.DB.Models;
using System.Collections.Generic;

namespace SpacePark.DB.Interfaces
{
    public interface IQuery
    {
        List<Spot> GetSpots();
        void CreateCustomer(Customer customer);
        Customer GetCustomer(string name);
        void CreateParkingStatus(ParkingStatus parkingStatus);
        IEnumerable<ParkingStatus> GetAllParkingStatus();
        ParkingStatus GetParkingStatusByCustomer(Customer customer);
        ParkingStatus GetParkingStatusByName(string name);
        void DeleteParkingStatusByName(ParkingStatus parkingStatus);
        void CreateShip(Ship ship);
        Ship GetShip(string plate);
    }
}