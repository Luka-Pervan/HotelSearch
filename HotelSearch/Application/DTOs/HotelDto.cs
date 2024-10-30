using System.ComponentModel.DataAnnotations;

namespace HotelSearch.Application.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public GeoLocationDto Location { get; set; }
    }
}
