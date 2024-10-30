using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSearch.Application.DTOs;
using HotelSearch.Application.Services;
using HotelSearch.Core.Entities;
using HotelSearch.Core.Helpers;
using AutoMapper;
using Xunit;
using HotelSearch.Application.Mappings;

public class HotelServiceTests
{
    private readonly IMapper _mapper;
    private readonly HotelService _hotelService;

    public HotelServiceTests()
    {
        // Setup AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new HotelMappingProfile());
        });
        _mapper = config.CreateMapper();

        // Reset in-memory data store before each test
        HotelDataStore.Hotels.Clear();
        HotelDataStore.Hotels.AddRange(GetTestHotels()); // Add some test hotels
        _hotelService = new HotelService(_mapper);
    }

    // Sample hotel data for testing
    private IEnumerable<Hotel> GetTestHotels()
    {
        return new List<Hotel>
        {
            new Hotel { Id = 1, Name = "Budget Suites", Price = 70, Location = new GeoLocation(34.0522, -118.2437) },
            new Hotel { Id = 2, Name = "City Center Stay", Price = 85, Location = new GeoLocation(34.0522, -118.2537) },
            new Hotel { Id = 3, Name = "Seaside Inn", Price = 95, Location = new GeoLocation(34.0522, -118.2637) },
            new Hotel { Id = 4, Name = "Luxury Palace", Price = 300, Location = new GeoLocation(34.0522, -118.2937) }
        };
    }

    // SearchHotelsAsync Tests
    [Fact]
    public async Task SearchHotelsAsync_ShouldReturnPagedResults()
    {
        var requestDto = new HotelSearchRequestDto
        {
            CurrentLatitude = 34.0522,
            CurrentLongitude = -118.2437,
            PageNumber = 1,
            PageSize = 2
        };

        var result = await _hotelService.SearchHotelsAsync(requestDto);

        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(4, result.TotalCount);
    }

    [Fact]
    public async Task SearchHotelsAsync_ShouldReturnSortedResults()
    {
        var requestDto = new HotelSearchRequestDto
        {
            CurrentLatitude = 34.0522,
            CurrentLongitude = -118.2437,
            PageNumber = 1,
            PageSize = 10
        };

        var result = await _hotelService.SearchHotelsAsync(requestDto);

        Assert.NotNull(result);
        Assert.Equal(4, result.Items.Count());
        Assert.Equal("Budget Suites", result.Items.First().Name);
    }

    [Fact]
    public async Task SearchHotelsAsync_ShouldReturnEmpty_WhenNoHotels()
    {
        HotelDataStore.Hotels.Clear();

        var requestDto = new HotelSearchRequestDto
        {
            CurrentLatitude = 34.0522,
            CurrentLongitude = -118.2437,
            PageNumber = 1,
            PageSize = 2
        };

        var result = await _hotelService.SearchHotelsAsync(requestDto);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    // GetHotelByIdAsync Tests
    [Fact]
    public async Task GetHotelByIdAsync_ShouldReturnHotel_WhenHotelExists()
    {
        var result = await _hotelService.GetHotelByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Budget Suites", result.Name);
    }

    [Fact]
    public async Task GetHotelByIdAsync_ShouldReturnNull_WhenHotelDoesNotExist()
    {
        var result = await _hotelService.GetHotelByIdAsync(999);

        Assert.Null(result);
    }

    // GetAllHotelsAsync Tests
    [Fact]
    public async Task GetAllHotelsAsync_ShouldReturnAllHotels()
    {
        var result = await _hotelService.GetAllHotelsAsync();

        Assert.NotNull(result);
        Assert.Equal(4, result.Count());
    }

    // CreateHotelAsync Tests
    [Fact]
    public async Task CreateHotelAsync_ShouldAddHotel_WhenValidHotelInfoProvided()
    {
        var newHotel = new HotelInfoDto
        {
            Name = "New Hotel",
            Price = 150,
            Location = new GeoLocationDto { Latitude = 34.0522, Longitude = -118.2437 }
        };

        var result = await _hotelService.CreateHotelAsync(newHotel);

        Assert.NotNull(result);
        Assert.Equal("New Hotel", result.Name);
        Assert.Equal(5, HotelDataStore.Hotels.Count);
    }

    [Fact]
    public async Task CreateHotelAsync_ShouldFail_WhenRequiredFieldsMissing()
    {
        var newHotel = new HotelInfoDto
        {
            // Missing Name,
            Price = 150, 
            Location = new GeoLocationDto { Latitude = 34.0522, Longitude = -118.2437 }
        };

        var exception = await Assert.ThrowsAsync<Exception>(() => _hotelService.CreateHotelAsync(newHotel));

        Assert.Contains("Name is required", exception.Message);
    }

    // UpdateHotelAsync Tests
    [Fact]
    public async Task UpdateHotelAsync_ShouldUpdateHotel_WhenHotelExists()
    {
        var updateHotel = new HotelInfoDto
        {
            Name = "Updated Hotel",
            Price = 200,
            Location = new GeoLocationDto { Latitude = 34.0522, Longitude = -118.2437 }
        };

        var result = await _hotelService.UpdateHotelAsync(1, updateHotel);

        Assert.NotNull(result);
        Assert.Equal("Updated Hotel", result.Name);
        Assert.Equal(200, result.Price);
    }

    [Fact]
    public async Task UpdateHotelAsync_ShouldReturnNull_WhenHotelDoesNotExist()
    {
        var updateHotel = new HotelInfoDto
        {
            Name = "Updated Hotel",
            Price = 200,
            Location = new GeoLocationDto { Latitude = 34.0522, Longitude = -118.2437 }
        };

        var result = await _hotelService.UpdateHotelAsync(999, updateHotel);

        Assert.Null(result);
    }

    // DeleteHotelAsync Tests
    [Fact]
    public async Task DeleteHotelAsync_ShouldReturnTrue_WhenHotelExists()
    {
        var result = await _hotelService.DeleteHotelAsync(1);

        Assert.True(result);
        Assert.Equal(3, HotelDataStore.Hotels.Count);
    }

    [Fact]
    public async Task DeleteHotelAsync_ShouldReturnFalse_WhenHotelDoesNotExist()
    {
        var result = await _hotelService.DeleteHotelAsync(999);

        Assert.False(result);
    }
}
