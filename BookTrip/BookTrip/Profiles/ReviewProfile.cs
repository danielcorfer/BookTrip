using AutoMapper;
using BookTrip.Models.ReviewModel;
using BookTrip.Models.ReviewModel.DTOs;

namespace BookTrip.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>().ForMember(r => r.HotelId, opt => opt.MapFrom(src => src.Hotel.Id));

        }
    }
}