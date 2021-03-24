using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark.DB.Models
{
    public class DBSpot
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public decimal Price { get; set; }

        public DBSpot(int ID, int size, decimal price)
        {
           this.ID = ID;
           this.Size = size;
           this.Price = price;
        }
    }
}