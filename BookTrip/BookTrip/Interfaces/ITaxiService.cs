using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.TaxiReservationModel.DTOs;
using BookTrip.Models.UserModel;

namespace BookTrip.Interfaces
{
    public interface ITaxiService
    {
        ResponseMessage CreateReservation(TaxiReservationDTO taxiReservationDTO, User user, out bool isReservationRegistered, out TaxiReservation taxiReservation);
        ResponseMessage DeleteReservation(string deletionToken, int taxiReservationId, out bool deletionSuccessful);
        TaxiReservation GetTaxiReservationById(int taxiReservationId);
        bool IsTaxiReservationTimeValid(DateTime departure, RoomReservation roomReservation);
    }
}