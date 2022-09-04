using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.UserModel.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using MimeKit;
using MimeKit.Text;

namespace BookTrip.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;
        private IUserService userService;
        private readonly IStringLocalizer<EmailService> stringLocalizer;
        public EmailService(IConfiguration config, IUserService userService, IStringLocalizer<EmailService> stringLocalizer)
        {
            this.config = config;
            this.userService = userService;
            this.stringLocalizer = stringLocalizer;
        }

        public void SendEmail(string userEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Environment.GetEnvironmentVariable("EmailConfig_EmailUsername")));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = $"{subject}";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"{body}"
            };
            using var smtp = new SmtpClient();
            //seznam.cz port 25 is insecure, it is not recommended to use it, but it works with it
            //from May 30th 2022 Google didnt support the use of third-party apps
            //or we can use https://ethereal.email/create 
            smtp.Connect(Environment.GetEnvironmentVariable("EmailConfig_EmailHost"), 25, SecureSocketOptions.StartTls);
            smtp.Authenticate(Environment.GetEnvironmentVariable("EmailConfig_EmailUsername"), Environment.GetEnvironmentVariable("EmailConfig_EmailPassword"));
            smtp.Send(email);
            smtp.Disconnect(true);

        }

        public ResponseMessage SendEmailWithNewPassword(EmailDTO request, out bool isEmailSent)
        {
            var newPassword = userService.ForgottenPassword(request);
            if (newPassword != null)
            {
                string subject = "Password reset BookTrip";
                string body = $"Your new password is: <h3>{newPassword}</h3>";
                SendEmail(request.UserEmail, subject, body);
                isEmailSent = true;
                return new ResponseMessage
                {
                    Message = stringLocalizer["SendEmailWithPassword"]
                };
            }
            isEmailSent = false;
            return new ResponseMessage
            {
                Message = stringLocalizer["EmailDoesNotExist"]
            };
        }
        public ResponseMessage SendEmailWithDeletionLink(TaxiReservation taxiReservation)
        {

            string subject = "Taxi Reservation BookTrip";
            string body = "" +
            $"<div>Your taxi reservation was successful.</div>" +
            $"<div> Departure from: { taxiReservation.DepartureAddress} Arrival location:{ taxiReservation.RequestedAddress}</div>" +
            $"<div> Time of Departure:{taxiReservation.Departure}</div>" +
            $"<a data-method=\"delete\" href= \"https://localhost:7216/api/taxi/{taxiReservation.DeletionToken}/{taxiReservation.Id}/\" > To cancel your reservation click here </a> ";

            SendEmail(taxiReservation.RoomReservation.User.Email, subject, body);
            return new ResponseMessage
            {
                Message = stringLocalizer["SendEmailWithPassword"]
            };
        }

        public void SendReservationConfirmationEmail(RoomReservation roomReservation)
        {
            string subject = "Reservation Confirmation BookTrip";
            string body = "" + $"<div>Your Reservation is confirmed.<div>" +
                               $"<div> Reservation starts: {roomReservation.ReservationStartsAt} Reservation ends:{roomReservation.ReservationEndsAt}</div>" +
                               $"<div> You have reserved {roomReservation.Room.RoomName} at {roomReservation.Room.Hotel.Name}.<div>" +
            $"<a data-method=\"delete\" href= \"https://localhost:7216/api/reservation/{roomReservation.DeletionToken}/{roomReservation.Id}/\" > To cancel your reservation click here </a> ";

            SendEmail(roomReservation.User.Email, subject, body);
        }
    }
}
