using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
