using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark.DB.Models
{
    public class Ship
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Plate { get; set; }
        public int CustomerID { get; set; }
        public int Length { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public Ship() { }
        public Ship(int id, string name, string plate, int customerID)
        {
            ID = id;
            Name = name;
            Plate = plate;
            CustomerID = customerID;
        }

        public void Create()
        {
            using var ctx = new SpaceParkDbContext();
            ctx.Ship.Add(this);
            ctx.SaveChanges();
        }

        public Ship GetByPlate(string plate)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Ship
                .SingleOrDefault(s => s.Plate.ToLower() == plate.ToLower());
        }

        public void Delete()
        {
            using var ctx = new SpaceParkDbContext();
            ctx.Ship.Remove(this);
            ctx.SaveChanges();
        }
    }
}


