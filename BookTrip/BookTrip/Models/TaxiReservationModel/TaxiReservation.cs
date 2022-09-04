using BookTrip.Models.ReservationModel;

namespace BookTrip.Models.TaxiReservationModel
{
    public class TaxiReservation
    {

        public int Id { get; set; }
        public RoomReservation RoomReservation { get; set; }
        public string DepartureAddress { get; set; }
        public string RequestedAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime Departure { get; set; }
        public bool IsActive { get; set; } = true;
        public string DeletionToken { get; set; }
        public TaxiReservation()
        {

        }

        public TaxiReservation(string departureAddress, string requestedAddress, DateTime departure, RoomReservation roomReservation)
        {
            DepartureAddress = departureAddress;
            RequestedAddress = requestedAddress;
            Departure = departure;
            RoomReservation = roomReservation;
        }
    }
}
