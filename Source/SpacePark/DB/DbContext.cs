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
                new DBSpot () {ID=1, Size=20, Price=120},
                new DBSpot () {ID=2, Size=20, Price=120},
                new DBSpot () {ID=3, Size=50, Price=280},
                new DBSpot () {ID=4, Size=50, Price=280},
                new DBSpot () {ID=5, Size=50, Price=280},
                new DBSpot () {ID=6, Size=100, Price=600},
                new DBSpot () {ID=7, Size=100, Price=600},
                new DBSpot () {ID=8, Size=100, Price=1600},
                new DBSpot () {ID=9, Size=1000, Price=8000}
            );
        }
    }
}
