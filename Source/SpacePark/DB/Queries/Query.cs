using System.Linq;
using System.Collections.Generic;
using SpacePark.DB.Interfaces;
using SpacePark.DB.Models;

namespace SpacePark.DB.Queries
{
    public class Query : IQuery
    {
        private SpaceParkDbContext _context;
        public Query(SpaceParkDbContext context)
        {
            _context = context;
        }

        public void CreateCustomer(Customer customer)
        {
            using var ctx = new SpaceParkDbContext();
            ctx.Customers.Add(customer);
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

            return ctx.Customers
                .SingleOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public void CreateParkingStatus(ParkingStatus parkingStatus) {
            using var ctx = new SpaceParkDbContext();

            ctx.ParkingStatuses.Add(parkingStatus);
            ctx.SaveChanges();
        }

        public List<ParkingStatus> GetAllParkingStatus() {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatuses.ToList();
        }

        public ParkingStatus GetParkingStatusByCustomer(Customer customer) {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatuses
                        .SingleOrDefault(p => p.CustomerID == customer.ID);
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