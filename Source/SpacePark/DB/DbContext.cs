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
        public DbSet<DBCustomer> Customer { get; set; }
        public DbSet<DBShip> Ship { get; set; }
        public DbSet<DBSpot> Spot { get; set; }
        public DbSet<DBParkingStatus> ParkingStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this._connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBSpot>().HasData(
                new DBSpot(1, 20, 120),
                new DBSpot(2, 20, 120),
                new DBSpot(3, 50, 280),
                new DBSpot(4, 50, 280),
                new DBSpot(5, 50, 280),
                new DBSpot(6, 100, 600),
                new DBSpot(7, 100, 600),
                new DBSpot(8, 100, 1600),
                new DBSpot(9, 1000, 8000)
            );
        }
    }
}
