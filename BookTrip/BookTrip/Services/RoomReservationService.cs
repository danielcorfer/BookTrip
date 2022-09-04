using BookTrip.Database;
using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.ReservationModel.DTOs;
using BookTrip.Models.UserModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BookTrip.Services
{
    public class RoomReservationService : IRoomReservationService
    {
        private readonly IDbContext database;
        private readonly IStringLocalizer<RoomReservationService> stringLocalizer;
        private readonly IRoomService roomService;
        private readonly ITokenService tokenService;

        public RoomReservationService(AppDbContext database, IStringLocalizer<RoomReservationService> stringLocalizer, IRoomService roomService, ITokenService tokenService)
        {
            this.database = database;
            this.stringLocalizer = stringLocalizer;
            this.roomService = roomService;
            this.tokenService = tokenService;
        }

        public RoomReservation GetRoomReservationById(int RoomReservationId)
        {
            return database.RoomReservations.Include(rr => rr.User).FirstOrDefault(rr => rr.Id == RoomReservationId);
        }
        public ResponseMessage CheckIfRoomReservationIsValid(RoomReservation roomReservation, out bool isValid)
        {
            if (roomReservation.ReservationStartsAt < DateTime.Now.AddDays(1))
            {
                isValid = false;
                return new ResponseMessage()
                {
                    Message = "The date of the reservation has to be at least one day in advance"
                };
            }
            if (roomReservation.ReservationStartsAt > roomReservation.ReservationEndsAt)
            {
                isValid = false;
                return new ResponseMessage()
                {
                    Message = "The date of the start of the reservation has to be sooner than the end"
                };
            }
            if (roomReservation.ReservationEndsAt < roomReservation.ReservationStartsAt.AddDays(1))
            {
                isValid = false;

                return new ResponseMessage()
                {
                    Message = "The reservation has to be at least for one day"
                };
            }
            if (database.RoomReservations.Any(rr => (rr.Room.RoomId == roomReservation.Room.RoomId) && (rr.ReservationStartsAt < roomReservation.ReservationStartsAt && rr.ReservationEndsAt > roomReservation.ReservationStartsAt) ||
                                                                                                       (rr.ReservationStartsAt < roomReservation.ReservationEndsAt && rr.ReservationEndsAt > roomReservation.ReservationStartsAt)))
            {
                isValid = false;
                return new ResponseMessage()
                {
                    Message = "there is already a reservation at that time"
                };
            }
            isValid = true;
            return new ResponseMessage()
            {
                Message = stringLocalizer["OKRoomReservation"]
            };

        }
        public ResponseMessage BookRoom(RoomReservationDTO request, User user, out bool isReservationRegistered, out RoomReservation roomReservation)
        {
            var room = roomService.GetRoomById(request.RoomId);
            var newReservation = new RoomReservation(user, room, request.NumberOfGuests, request.ReservationStartsAt, request.ReservationEndsAt);
            if (newReservation == null)
            {
                isReservationRegistered = false;
                roomReservation = null;
                return new ResponseMessage()
                {
                    Message = stringLocalizer["ErrRoomReservation"]

                };
            }

            ResponseMessage responseMessage = CheckIfRoomReservationIsValid(newReservation, out bool isValid);
            if (isValid)
            {
                newReservation.DeletionToken = tokenService.CreateEmailToken(newReservation.Id);
                database.RoomReservations.Add(newReservation);
                database.SaveChanges();
                room.IsBooked = true;
                isReservationRegistered = true;
                roomReservation = newReservation;
                return responseMessage;
            }

            isReservationRegistered = false;
            roomReservation = null;
            return responseMessage;
        }

        public ResponseMessage DeleteRoomReservation(string deletionToken, int roomReservationId, out bool deletionSuccessful)
        {
            var roomReservation = GetRoomReservationById(roomReservationId);
            if (roomReservation != null && roomReservation.DeletionToken == deletionToken)
                roomReservation.IsActive = false;
            database.SaveChanges();
            deletionSuccessful = true;
            return new ResponseMessage()
            {
                Message = stringLocalizer["OkDeletionRoomReservation"]
            };
            deletionSuccessful = false;
            return new ResponseMessage()
            {
                Message = stringLocalizer["InvalidDeletionRoomReservation"]
            };
        }
    }
}
