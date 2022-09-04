using AutoMapper;
using BookTrip.Models.UserModel;
using BookTrip.Models.UserModel.DTOs;

namespace BookTrip.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
