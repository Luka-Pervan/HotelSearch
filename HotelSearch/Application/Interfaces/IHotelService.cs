using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSearch.API.Models;
using HotelSearch.Application.DTOs;

namespace HotelSearch.Application.Interfaces
{
    public interface IHotelService
    {
        Task<PagedResponse<HotelSearchResponseDto>> SearchHotelsAsync(HotelSearchRequestDto request);
        Task<HotelDto> GetHotelByIdAsync(int id);
        Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
        Task<HotelDto> CreateHotelAsync(HotelInfoDto hotel);
        Task<HotelDto> UpdateHotelAsync(int id, HotelInfoDto hotel);
        Task<bool> DeleteHotelAsync(int id);
    }
}
