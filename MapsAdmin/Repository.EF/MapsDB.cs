using System.Data.Entity;
using Entities;

namespace Repository.EF
{
    public class MapsDB : DbContext
    {
        static MapsDB()
        {
            System.Data.Entity.Infrastructure.Database.SetInitializer<MapsDB>(null);
        }

        public MapsDB()
            : base("MapsDB")
        {
            

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(System.Data.Entity.ModelConfiguration.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
