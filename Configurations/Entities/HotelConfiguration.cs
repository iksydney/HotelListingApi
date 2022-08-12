using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
    }
}
