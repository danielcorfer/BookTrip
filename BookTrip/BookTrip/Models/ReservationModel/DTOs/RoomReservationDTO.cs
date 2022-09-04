namespace BookTrip.Models.ReservationModel.DTOs
{
    public class RoomReservationDTO
    {
        public int RoomId { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime ReservationStartsAt { get; set; }
        public DateTime ReservationEndsAt { get; set; }
    }
}
