using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSearch.Application.DTOs;
using HotelSearch.Application.Interfaces;
using HotelSearch.Core.Entities;
using HotelSearch.Core.Helpers;
using AutoMapper;

namespace HotelSearch.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IMapper _mapper;

        // Constructor no longer requires IRepository<Hotel>
        public HotelService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PagedResponse<HotelSearchResponseDto>> SearchHotelsAsync(HotelSearchRequestDto request)
        {
            var hotels = HotelDataStore.Hotels;

            var searchResults = hotels
                .Select(hotel => new HotelSearchResponseDto
                {
                    Name = hotel.Name,
                    Price = hotel.Price,
                    DistanceFromUser = GeoDistanceCalculator.CalculateDistance(
                        request.CurrentLatitude, request.CurrentLongitude,
                        hotel.Location.Latitude, hotel.Location.Longitude)
                })
                .OrderBy(h => h.Price)
                .ThenBy(h => h.DistanceFromUser);

            // Get total count before applying pagination
            var totalCount = searchResults.Count();

            // Apply pagination
            var pagedResults = searchResults
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // Create and return paged response
            return new PagedResponse<HotelSearchResponseDto>(pagedResults, totalCount, request.PageNumber, request.PageSize);
        }


        public async Task<HotelDto> GetHotelByIdAsync(int id)
        {
            var hotel = HotelDataStore.Hotels.FirstOrDefault(h => h.Id == id);
            return await Task.FromResult(_mapper.Map<HotelDto>(hotel));

            // Replace with: var hotel = await _hotelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
        {
            var hotels = HotelDataStore.Hotels;
            return await Task.FromResult(_mapper.Map<IEnumerable<HotelDto>>(hotels));

            // Replace with: var hotels = await _hotelRepository.GetAllAsync();
        }

        public async Task<HotelDto> CreateHotelAsync(HotelInfoDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);

            // Generate a new unique ID (in a real database, this would be auto-generated)
            hotel.Id = HotelDataStore.Hotels.Any()
                ? HotelDataStore.Hotels.Max(h => h.Id) + 1
                : 1;

            HotelDataStore.Hotels.Add(hotel);
            return await Task.FromResult(_mapper.Map<HotelDto>(hotel));

            // Replace with: var result = await _hotelRepository.AddAsync(hotel);
            // After replacement, remove ID generation logic as the database will handle it
        }

        public async Task<HotelDto> UpdateHotelAsync(int id, HotelInfoDto hotelDto)
        {
            var hotel = HotelDataStore.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return null;

            // Update properties
            hotel.Name = hotelDto.Name;
            hotel.Price = hotelDto.Price;
            hotel.Location = new GeoLocation(hotelDto.Location.Latitude, hotelDto.Location.Longitude);

            return await Task.FromResult(_mapper.Map<HotelDto>(hotel));

            // Replace with: var result = await _hotelRepository.UpdateAsync(hotel);
        }

        public async Task<bool> DeleteHotelAsync(int id)
        {
            var hotel = HotelDataStore.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return false;

            HotelDataStore.Hotels.Remove(hotel);
            return await Task.FromResult(true);

            // Replace with: return await _hotelRepository.DeleteAsync(id);
        }

    }
}
