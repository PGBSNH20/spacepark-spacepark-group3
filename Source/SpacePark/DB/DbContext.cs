using Microsoft.EntityFrameworkCore;
using SpacePark.Models;
using SpacePark.DB.Models;


namespace SpacePark.DB
{
    public class AppDbContext : DbContext
    {
        private string _connString;
        public AppDbContext() : base()
        {
            this._connString = "host=localhost;port=5432;database=SpacePark;user id=admin;password=secret";
        }
        public DbSet<DBUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this._connString);
        }

    }
}
