using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SpacePark.DB.Models
{
    public class ParkingStatus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime arrivalTime { get; set; }
        [Required]
        public DateTime departureAt { get; set; }
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual DBCustomer Customer { get; set; }
        
        public int SpotID { get; set; }

        [ForeignKey("SpotID")]
        public virtual DBSpot Spot { get; set; }
    }
}