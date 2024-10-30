using HotelSearch.Application.DTOs;
using HotelSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    // 1. Search hotels
    [HttpGet("search")] // Unique route
    public async Task<IActionResult> SearchHotels([FromQuery] HotelSearchRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var hotels = await _hotelService.SearchHotelsAsync(request);
        return Ok(hotels);
    }

    // 2. Get hotel by ID
    [HttpGet("{id}")] // Unique route
    public async Task<IActionResult> GetHotel(int id)
    {
        var hotel = await _hotelService.GetHotelByIdAsync(id);
        if (hotel == null) return NotFound();
        return Ok(hotel);
    }

    // 3. Create a new hotel
    [HttpPost] // Unique route
    public async Task<IActionResult> CreateHotel([FromBody] HotelInfoDto createHotelDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdHotel = await _hotelService.CreateHotelAsync(createHotelDto);
        return CreatedAtAction(nameof(GetHotel), new { id = createdHotel.Id }, createdHotel);
    }


    // 4. Update an existing hotel
    [HttpPut("{id}")] // Unique route
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelInfoDto hotelDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedHotel = await _hotelService.UpdateHotelAsync(id, hotelDto);
        if (updatedHotel == null) return NotFound();
        return Ok(updatedHotel);
    }

    // 5. Delete a hotel by ID
    [HttpDelete("{id}")] // Unique route
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var result = await _hotelService.DeleteHotelAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
