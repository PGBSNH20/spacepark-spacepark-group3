using System;
using System.ComponentModel.DataAnnotations;

namespace SpacePark.DB.Models
{
    public class DBCustomer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}