using BookTrip.Database;
using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.HotelModel.RoomModel.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BookTrip.Services
{
    public class RoomService : IRoomService
    {
        private readonly IDbContext database;
        private readonly IStringLocalizer<RoomService> stringLocalizer;
        private readonly IHotelService hotelService;
        public RoomService(IDbContext database, IStringLocalizer<RoomService> stringLocalizer, IHotelService hotelService)
        {
            this.database = database;
            this.stringLocalizer = stringLocalizer;
            this.hotelService = hotelService;
        }

        private bool ParametersNotPresent(params string[] input)
        {
            return !input.All(i => i != null);
        }

        private bool ParametersNotPresent<T>(T dto) where T : class
        {
            return !dto.GetType().GetProperties().All(prop => prop.GetValue(dto) != null);
        }

        public Room GetRoomByName(string roomName)
        {
            if (ParametersNotPresent(roomName))
            {
                return null;
            }
            return database.Rooms.Where(r => r.RoomName == roomName).Include(r => r.RoomBeds).SingleOrDefault(r => r.RoomName == roomName);
        }

        public Room GetRoomById(int roomId)
        {
            if (ParametersNotPresent(roomId.ToString()))
            {
                return null;
            }
            return database.Rooms.Where(r => r.RoomId == roomId).Include(r => r.RoomBeds).Include(r => r.RoomReservations).SingleOrDefault(r => r.RoomId == roomId);
        }

        public ResponseMessage AddOneRoom(RoomDTO input)
        {
            if (ParametersNotPresent(input))
            {
                return new ResponseMessage
                {
                    Message = stringLocalizer["ParamsMissing"]
                };
            }
            var hotel = hotelService.GetHotelByHotelId(input.HotelId);
            if (database.Hotels.FirstOrDefault(h => h == hotel).Rooms.Any(r => r.RoomName == input.RoomName))
            {
                return new ResponseMessage
                {
                    Message = stringLocalizer["ErrRoomExists"]
                };
            }

            database.Rooms.Add(new Room(input.Price, hotel, input.RoomName));
            database.SaveChanges();
            return new ResponseMessage
            {
                Message = stringLocalizer["SuccAddRoom"]
            };
        }

        public ResponseMessage AddMultipleBedsToRoom(RoomMultipleDTO input)
        {

            if (ParametersNotPresent(input))
            {
                return new ResponseMessage
                {
                    Message = stringLocalizer["ParamsMissing"]
                };
            }
            if (database.Rooms.Any(r => r.RoomName == input.roomName))
            {
                return new ResponseMessage
                {
                    Message = stringLocalizer["ErrRoomExists"]
                };
            }

            var hotel = hotelService.GetHotelByHotelId(input.hotelId);
            database.Rooms.Add(new Room(input.price, hotel, input.numberOfBeds, input.bedName, input.roomName, input.customSpotsBed));
            database.SaveChanges();
            return new ResponseMessage
            {
                Message = stringLocalizer["SuccAddRoom"]
            };
        }
    }
}
