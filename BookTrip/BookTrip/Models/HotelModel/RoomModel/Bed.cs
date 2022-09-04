namespace BookTrip.Models.HotelModel.RoomModel
{
    public abstract class Bed
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int NumberOfSleepingSpots { get; set; }
        public List<RoomBed> RoomBeds { get; set; }

        internal Bed()
        {
        }
    }
}
