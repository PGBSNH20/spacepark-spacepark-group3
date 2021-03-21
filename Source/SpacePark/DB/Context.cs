using Microsoft.EntityFrameworkCore;
using SpacePark.Models;


namespace SpacePark
{
    public class Context : DbContext
    {
        private string _connString;
        public Context(Config config) : base()
        {
            this._connString = config.ConnectionString;
        }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this._connString);
        }

    }
}
