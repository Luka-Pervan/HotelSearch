using AutoMapper;
using HotelSearch.Application.DTOs;
using HotelSearch.Core.Entities;

namespace HotelSearch.Application.Mappings
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            // Mapping between HotelDto and Hotel (for general usage)
            CreateMap<HotelDto, Hotel>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            // Mapping between HotelInfoDto and Hotel (for creating new hotels)
            CreateMap<HotelInfoDto, Hotel>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            // Mapping between GeoLocationDto and GeoLocation (bidirectional)
            CreateMap<GeoLocationDto, GeoLocation>().ReverseMap();
        }
    }

}
