using System.ComponentModel.DataAnnotations;

namespace HotelSearch.Application.DTOs
{
    public class HotelInfoDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public GeoLocationDto Location { get; set; }
    }
}
