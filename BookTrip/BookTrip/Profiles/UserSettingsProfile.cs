using AutoMapper;
using BookTrip.Models.UserModel;
using BookTrip.Models.UserModel.DTOs;

namespace BookTrip.Profiles
{
    public class UserSettingsProfile : Profile
    {
        public UserSettingsProfile()
        {
            CreateMap<UserSettings, UserSettingsDTO>();
        }
    }
}
