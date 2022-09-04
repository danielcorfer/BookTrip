using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.UserModel.DTOs;

namespace BookTrip.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string userEmail,string subject, string body);
        ResponseMessage SendEmailWithNewPassword(EmailDTO request, out bool isEmailSent);
        ResponseMessage SendEmailWithDeletionLink(TaxiReservation taxiReservation);
        void SendReservationConfirmationEmail(RoomReservation roomReservation);
    }
}
