using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SpacePark.DB.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public Customer() { }
        public Customer(int ID, string name)
        {
            this.ID = ID;
            this.Name = name;
        }
        public Customer(string name)
        {
            this.Name = name;
        }

        public int Create()
        {
            using var ctx = new SpaceParkDbContext();
            var data = ctx.Customer.Add(this);
            ctx.SaveChanges();
            return data.Entity.ID;
        }

        public static Customer GetByName(string name)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Customer
                .SingleOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public static Customer GetByID(int id)
        {
            using var ctx = new SpaceParkDbContext();
            return ctx.Customer
                .SingleOrDefault(x => x.ID == id);
        }
        public void Delete()
        {
            using var ctx = new SpaceParkDbContext();
            ctx.Customer.Remove(this);
            ctx.SaveChanges();
        }
    }
}