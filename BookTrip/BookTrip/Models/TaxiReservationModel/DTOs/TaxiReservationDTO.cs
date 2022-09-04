namespace BookTrip.Models.TaxiReservationModel.DTOs
{
    public class TaxiReservationDTO
    {
        public int RoomReservationId { get; set; }
        public string DepartureAddress { get; set; }
        public string RequestedAddress { get; set; }
        public DateTime Departure { get; set; }
    }
}
