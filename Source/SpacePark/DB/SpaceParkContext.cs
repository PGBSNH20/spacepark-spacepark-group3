using Microsoft.EntityFrameworkCore;
using SpacePark.DB.Models;

namespace SpacePark.DB
{
    public class SpaceParkDbContext : DbContext
    {
        private readonly string _connString = "host=localhost;port=5432;database=SpacePark;user id=admin;password=secret";
        public SpaceParkDbContext() : base()
        {
            //this._connString = AppConfig.GetConfig().ConnectionString;
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Ship> Ship { get; set; }
        public DbSet<Spot> Spot { get; set; }
        public DbSet<ParkingStatus> ParkingStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this._connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spot>()
            .HasData(
                new Spot { ID = 1, Size = 20, Price = 120 },
                new Spot { ID = 2, Size = 20, Price = 120 },
                new Spot { ID = 3, Size = 100, Price = 280 },
                new Spot { ID = 4, Size = 250, Price = 280 },
                new Spot { ID = 5, Size = 500, Price = 280 },
                new Spot { ID = 6, Size = 1000, Price = 600 },
                new Spot { ID = 7, Size = 10000, Price = 600 },
                new Spot { ID = 8, Size = 10000, Price = 1600 },
                new Spot { ID = 9, Size = 20000, Price = 8000 });
        }
    }
}
