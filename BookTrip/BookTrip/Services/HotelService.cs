using BookTrip.Database;
using BookTrip.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using AutoMapper;
using BookTrip.Models.General;
using BookTrip.Models.HotelModel;
using BookTrip.Models.UserModel.DTOs;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.HotelModel.DTOs;
using BookTrip.Models.UserModel;
using BookTrip.Models;

namespace BookTrip.Services
{
    public class HotelService : IHotelService
    {
        private readonly IMapper mapper;
        private readonly IDbContext database;
        private readonly IPropertyTypeService propertyService;
        private readonly IUserService userService;
        private readonly IStringLocalizer<HotelService> stringLocalizer;
        public HotelService(IDbContext database, IPropertyTypeService propertyService, IUserService userService, IStringLocalizer<HotelService> stringLocalizer, IMapper mapper)        
        {
            this.database = database;
            this.propertyService = propertyService;
            this.userService = userService;
            this.stringLocalizer = stringLocalizer;
            this.mapper = mapper;
        }

        public bool HotelExists(string name, string address)
        {
            return database.Hotels.Any(h => h.Name == name && h.Address == address);
        }

        private Hotel NewHotel(string name, string location, string country, string region, string city, string address, string description, int pricePerNight, int starRating, string propertyType)
        {
            var newHotel = new Hotel(name, location, country, region, city, address, description, pricePerNight, propertyService.GetPropType(propertyType));
            database.Hotels.Add(newHotel);
            database.SaveChanges();
            return newHotel;
        }

        public bool IsUserHotelManager(string user)
        {
            return userService.GetUserByEmail(user).Role == Enums.RoleEnum.Manager.ToString();
        }

        public bool IsUserAdmin(string user)
        {
            return userService.GetUserByEmail(user).Role == Enums.RoleEnum.Admin.ToString();
        }

        public ResponseMessage AddNewHotel(UserHotelDTO input, User currentUser)
        {
            if (currentUser != null)
            {
                if (HotelExists(input.Name, input.Address))
                    return new ResponseMessage
                    {
                        Message = stringLocalizer["ErrorHotelExists"]
                    };

                NewHotel(input.Name, input.Location, input.Country, input.Region, input.City, input.Address, input.Description, input.PricePerNight, input.StarRating, input.PropertyType);
                return new ResponseMessage
                {
                    Message = stringLocalizer["SuccAddHotel"]
                };

            }
            return new ResponseMessage
            {
                Message = stringLocalizer["NoPermission"]
            };
        }

        public List<Room> GetAllRoomsInHotel(int hotelId)
        {
            return database.Hotels.Where(h => h.Id == hotelId).SelectMany(h => h.Rooms).Include(r => r.RoomBeds).ToList();
        }

        public List<HotelDTO> Search(string city, int capacity)
        {
            List<HotelDTO> output = new();
            output = database.Hotels.Where(h => h.City.Contains(city)).Include(h => h.User).Include(h => h.PropertyType).Include(h => h.Rooms.Where(r => r.NumberOfGuestsLivingIn >= capacity)).Select(h => mapper.Map<HotelDTO>(h)).ToList();
            return output;
        }

        public object HotelDetailsMessage(int id)
        {
            var hotel = database.Hotels.Where(h=>h.Id == id).Include(h => h.PropertyType).Include(h=>h.Reviews).ThenInclude(r=>r.User).FirstOrDefault();
            if (hotel == null)
            {
                return new ResponseMessage
                {
                    Message = stringLocalizer["ErrorHotelDoesntExist"]
                };
            }
            return mapper.Map<HotelDTO>(hotel);
        }

        public ResponseMessage EditHotelDetails(string hotelId, EditHotelDTO input)
        {
            if (ParametersNotPresent(input))
            {
                return new ResponseMessage
                {
                    Message = stringLocalizer["MissingParams"]
                };
            }
            var hotel = GetHotelByHotelId(int.Parse(hotelId));
            ChangeDetail(hotel, input);            

            return new ResponseMessage
            {
                Message = stringLocalizer["SuccEditHotel"]
            };
        }

        public List<HotelDTO> ListHotels(Guid id)
        {
            List<HotelDTO> output = new();

            output = database.Hotels.Where(h => h.User.Id == id).Include(h => h.PropertyType).Select(h => mapper.Map<HotelDTO>(h)).ToList();

            return output;
        }

        public bool GetHotelByUserId(Guid managerId)
        {
            return database.Hotels.Include(h=>h.Reviews).FirstOrDefault(h => h.User.Id == managerId) == null;
        }

        public Hotel GetHotelByHotelId(int hotelId)
        {
            return database.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == hotelId);
        }
        
        public Hotel SetTimeZone(Hotel hotel, string tzSelection)
        {
            hotel.TimeZoneId = TimeZoneInfo.GetSystemTimeZones().Where(tz => tz.DisplayName.Contains(tzSelection)).FirstOrDefault().Id;
            database.SaveChanges();
            return hotel;
        }

        private void ChangeDetail(Hotel hotel, EditHotelDTO input)
        {
            hotel.Name = input.Name;
            hotel.Location = input.Location;
            hotel.Country = input.Country;
            hotel.Region = input.Region;
            hotel.City = input.City;
            hotel.Address = input.Address;
            hotel.Description = input.Description;
            hotel.PricePerNight = input.PricePerNight;
            SetTimeZone(hotel, input.TimeZone);
            database.SaveChanges();
        }
        
        private bool ParametersNotPresent<T>(T dto) where T : class
        {
            return !dto.GetType().GetProperties().All(prop => prop.GetValue(dto) != null);
        }
        
        public DateTime ReadTime(DateTime input, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(input, timeZoneId);
        }
    }
}

