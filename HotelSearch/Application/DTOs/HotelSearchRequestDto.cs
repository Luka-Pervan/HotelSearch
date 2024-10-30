using System.ComponentModel.DataAnnotations;

namespace HotelSearch.Application.DTOs
{
    public class HotelSearchRequestDto
    {
        [Required]
        public double CurrentLatitude { get; set; }
        [Required]
        public double CurrentLongitude { get; set; }
        public int PageNumber { get; set; } = 1; // Default to the first page
        public int PageSize { get; set; } = 10; // Default number of items per page

    }
}
