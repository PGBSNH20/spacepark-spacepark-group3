using System.Linq;
using System.Collections.Generic;
using SpacePark.DB.Interfaces;
using SpacePark.DB.Models;

namespace SpacePark.DB.Queries
{
    public class Query : IQuery
    {
        public void CreateCustomer(Customer customer)
        {
            using var ctx = new SpaceParkDbContext();
            ctx.Customer.Add(customer);
            ctx.SaveChanges();
        }

        public void CreateShip(Ship ship)
        {
            using var ctx = new SpaceParkDbContext();
            ctx.Ship.Add(ship);
            ctx.SaveChanges();
        }

        public Customer GetCustomer(string name)
        {

            using var ctx = new SpaceParkDbContext();

            return ctx.Customer
                .SingleOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public void CreateParkingStatus(ParkingStatus parkingStatus)
        {
            using var ctx = new SpaceParkDbContext();

            ctx.ParkingStatus.Add(parkingStatus);
            ctx.SaveChanges();
        }

        public IEnumerable<ParkingStatus> GetAllParkingStatus()
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus.ToList();
        }

        public ParkingStatus GetParkingStatusByCustomer(Customer customer)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus
                        .SingleOrDefault(p => p.CustomerID == customer.ID);
        }

        public ParkingStatus GetParkingStatusByName(string name)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus
                        .SingleOrDefault(p => p.Customer.Name == name);
        }

        public void DeleteParkingStatusByName(ParkingStatus parkingStatus)
        {
            using var ctx = new SpaceParkDbContext();
            ctx.ParkingStatus.Remove(parkingStatus);
            ctx.SaveChanges();
        }

        public Ship GetShip(string plate)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Ship
                .SingleOrDefault(x => x.Plate.ToLower() == plate.ToLower());
        }

        public List<Spot> GetSpots()
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Spot.ToList();
        }
    }
}