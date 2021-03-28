using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark.DB.Models
{
    public class Spot
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public decimal Price { get; set; }

        public static IEnumerable<Spot> GetAll()
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Spot.ToList();
        }

        public static Spot GetByID(int id)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Spot
                .SingleOrDefault(x => x.ID == id);
        }
    }
}