using AutoMapper;
using BookTrip.Models.HotelModel.DTOs;
using BookTrip.Models.HotelModel;

namespace BookTrip.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelDTO>().ForMember(d => d.Type, opt => opt.MapFrom(src => src.PropertyType.Type));
        }
    }
}
