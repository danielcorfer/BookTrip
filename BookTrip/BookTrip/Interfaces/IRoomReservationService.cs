using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.ReservationModel.DTOs;
using BookTrip.Models.UserModel;

namespace BookTrip.Interfaces
{
    public interface IRoomReservationService
    {
        ResponseMessage BookRoom(RoomReservationDTO request, User user, out bool isReservationRegistered, out RoomReservation roomReservation);
        RoomReservation GetRoomReservationById(int RoomReservationId);
        ResponseMessage CheckIfRoomReservationIsValid(RoomReservation roomReservation, out bool isValid);
        ResponseMessage DeleteRoomReservation(string deletionToken, int roomReservationId, out bool deletionSuccessful);

    }
}