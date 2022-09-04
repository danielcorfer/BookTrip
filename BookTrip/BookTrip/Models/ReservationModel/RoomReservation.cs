using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.UserModel;


namespace BookTrip.Models.ReservationModel
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
        public int NumberOfGuests { get; set; }
        public List<TaxiReservation> TaxiReservations { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime ReservationStartsAt { get; set; }
        public DateTime ReservationEndsAt { get; set; }
        public string DeletionToken { get; set; }
        public bool IsActive { get; set; } = true;

        private RoomReservation()
        {


        }
        public RoomReservation(User user, Room room, int numberOfGuest, DateTime reservationStartsAt, DateTime reservationEndsAt)
        {
            User = user;
            Room = room;
            NumberOfGuests = numberOfGuest;
            ReservationStartsAt = reservationStartsAt;
            ReservationEndsAt = reservationEndsAt;
            TaxiReservations = new List<TaxiReservation>();
        }
    }
}
