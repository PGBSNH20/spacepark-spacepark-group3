using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark.DB.Models
{
    public class DBShip
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Plate { get; set; }
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual DBCustomer Customer { get; set; }
    }
}


