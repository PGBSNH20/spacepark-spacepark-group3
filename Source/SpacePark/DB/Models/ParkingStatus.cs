using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SpacePark.DB.Models
{
    public class ParkingStatus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public int SpotID { get; set; }

        [ForeignKey("SpotID")]
        public virtual Spot Spot { get; set; }

        public ParkingStatus() {}
        public ParkingStatus(int ID, DateTime arrivalTime, int customerID, int spotID) {
            this.ID = ID;
            this.ArrivalTime = arrivalTime;
            this.CustomerID = customerID;
            this.SpotID = spotID;
        }

        public void Create() {
            using var ctx = new SpaceParkDbContext();
            ctx.ParkingStatus.Add(this);
            ctx.SaveChanges();
        }

        public IEnumerable<ParkingStatus> GetAll() {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus.ToList();
        }

        public ParkingStatus GetByCusomterID(int id) {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus
                .SingleOrDefault(p => p.CustomerID == id);
        }

        public ParkingStatus GetByCusomterName(string name) {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus
                .SingleOrDefault(p => p.Customer.Name.ToLower() == name.ToLower());
        }
        public ParkingStatus GetBySpotID(int id) {
            using var ctx = new SpaceParkDbContext();
            return ctx.ParkingStatus
                .SingleOrDefault(p => p.SpotID == id);
        }

        public void Delete() {
            using var ctx = new SpaceParkDbContext();
            ctx.ParkingStatus.Remove(this);
            ctx.SaveChanges();
        }
    }
}