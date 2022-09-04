using BookTrip.Database;
using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.TaxiReservationModel.DTOs;
using BookTrip.Models.UserModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BookTrip.Services
{
    public class TaxiService : ITaxiService
    {
        private readonly IDbContext database;
        private readonly IStringLocalizer<TaxiService> stringLocalizer;
        private readonly IRoomReservationService roomReservationService;
        private readonly ITokenService tokenService;

        public TaxiService(IDbContext database, IStringLocalizer<TaxiService> stringLocalizer, IRoomReservationService roomReservationService, ITokenService tokenService)
        {
            this.database = database;
            this.stringLocalizer = stringLocalizer;
            this.roomReservationService = roomReservationService;
            this.tokenService = tokenService;
        }
        public ResponseMessage CreateReservation(TaxiReservationDTO taxiReservationDTO, User user, out bool isReservationRegistered, out TaxiReservation taxiReservation)
        {
            var roomReservation = roomReservationService.GetRoomReservationById(taxiReservationDTO.RoomReservationId);

            if (roomReservation != null && roomReservation.User.Id == user.Id)
            {
                if (IsTaxiReservationTimeValid(taxiReservationDTO.Departure, roomReservation))
                {
                    var reservation = new TaxiReservation(taxiReservationDTO.DepartureAddress, taxiReservationDTO.RequestedAddress, taxiReservationDTO.Departure, roomReservation);
                    reservation.DeletionToken = tokenService.CreateEmailToken(reservation.Id, reservation.Departure);
                    database.TaxiReservations.Add(reservation);
                    database.SaveChanges();
                    isReservationRegistered = true;
                    taxiReservation = reservation;
                    return new ResponseMessage()
                    {
                        Message = stringLocalizer["OkTaxiReservation"]
                    };
                }
                isReservationRegistered = false;
                taxiReservation = null;
                return new ResponseMessage()
                {
                    Message = stringLocalizer["InvalidTimeTaxiReservation"]
                };
            }
            isReservationRegistered = true;
            taxiReservation = null;
            return new ResponseMessage()
            {
                Message = stringLocalizer["InvalidTaxiReservation"]
            };
        }
        public TaxiReservation GetTaxiReservationById(int taxiReservationId)
        {
            return database.TaxiReservations.Include(tr => tr.RoomReservation).ThenInclude(rr => rr.User).FirstOrDefault(tr => tr.Id == taxiReservationId);
        }
        public ResponseMessage DeleteReservation(string deletionToken, int taxiReservationId, out bool deletionSuccessful)
        {
            TaxiReservation taxiReservation = GetTaxiReservationById(taxiReservationId);
            if (taxiReservation != null && taxiReservation.DeletionToken == deletionToken)
            {
                taxiReservation.IsActive = false;
                database.SaveChanges();
                deletionSuccessful = true;
                return new ResponseMessage()
                {
                    Message = stringLocalizer["OkDeletionTaxiReservation"]
                };
            }
            deletionSuccessful = false;
            return new ResponseMessage()
            {
                Message = stringLocalizer["InvalidDeletionTaxiReservation"]
            };
        }

        public bool IsTaxiReservationTimeValid(DateTime departure, RoomReservation roomReservation)
        {
            return (departure > roomReservation.ReservationStartsAt && departure <= roomReservation.ReservationEndsAt);
        }
    }
}
