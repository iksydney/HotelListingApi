using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}
