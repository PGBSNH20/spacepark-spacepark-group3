using Microsoft.EntityFrameworkCore;
using SpacePark.Models;


namespace SpacePark
{
    public class Context : DbContext
    {
        public Context() : base()
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Data Source=localhost\\SQLEXPRESS, 1433;Initial Catalog=SpacePark;Trusted_Connection=True;");
        }

    }
}
