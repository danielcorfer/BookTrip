using BookTrip.Models.General;
using BookTrip.Models.HotelModel;
using BookTrip.Models.HotelModel.DTOs;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.UserModel;
using BookTrip.Models.UserModel.DTOs;

namespace BookTrip.Interfaces
{
    public interface IHotelService
    {
        ResponseMessage AddNewHotel(UserHotelDTO input, User currentUser);
        List<Room> GetAllRoomsInHotel(int hotelId);
        Hotel GetHotelByHotelId(int hotelId);
        bool GetHotelByUserId(Guid managerId);
        object HotelDetailsMessage(int id);
        public ResponseMessage EditHotelDetails(string hotelId, EditHotelDTO input);
        bool HotelExists(string name, string address);
        bool IsUserHotelManager(string user);
        List<HotelDTO> ListHotels(Guid id);
        Hotel SetTimeZone(Hotel hotel, string tzSelection);
        List<HotelDTO> Search(string city, int capacity);
    }
}