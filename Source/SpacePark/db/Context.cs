using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


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
            optionsBuilder.UseSqlServer(@"Data Source=localhost\\SQLEXPRESS, 1433;Initial Catalog=SpacePark;Trusted_Connection=True;");
        }

    }
}
