using BookTrip.Models.General;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.HotelModel.RoomModel.DTOs;

namespace BookTrip.Interfaces
{
    public interface IRoomService
    {
        ResponseMessage AddMultipleBedsToRoom(RoomMultipleDTO input);
        ResponseMessage AddOneRoom(RoomDTO input);
        Room GetRoomById(int roomId);
        Room GetRoomByName(string roomName);
        
    }
}