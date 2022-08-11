using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Nigeria",
                    ShortName = "NIG"
                },
                new Country
                {
                    Id = 2,
                    Name = "United States Of America",
                    ShortName = "USA"
                },
                new Country
                {
                    Id = 3,
                    Name = "Ghana",
                    ShortName = "GH"
                },
                new Country
                {
                    Id = 4,
                    Name = "Canada",
                    ShortName = "CAN"
                }
                );
            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Address = "Terhemba Street",
                    CountryId = 1,
                    Rating = 4.5
                }, new Hotel
                {
                    Id = 2,
                    Address = "Olorunfunmi Street",
                    CountryId = 2,
                    Rating = 5.0
                },
                new Hotel
                {
                    Id = 3,
                    Address = "Goddy Street",
                    CountryId = 3,
                    Rating = 3.8
                }
                );
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
