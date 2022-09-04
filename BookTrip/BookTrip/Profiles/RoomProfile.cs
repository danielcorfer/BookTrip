using AutoMapper;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.HotelModel.RoomModel.DTOs;

namespace BookTrip.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>().ForMember(r => r.HotelId, opt => opt.MapFrom(src => src.Hotel.Id))
                .ForMember(r => r.Capacity, opt => opt.MapFrom(src => src.NumberOfGuestsLivingIn));
        }
    }
}
