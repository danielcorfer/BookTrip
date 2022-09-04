namespace BookTrip.Models.HotelModel.RoomModel
{
    public class RoomBed
    {
        public int Id { get; set; }
        public int AmountOfBeds { get; set; }
        public Bed Bed { get; set; }
        public Room Room { get; set; }

        private RoomBed()
        { }

        public RoomBed(Room room, Bed bed, int amountOfBeds)
        {
            Room = room;
            Bed = bed;
            AmountOfBeds = amountOfBeds;
        }
    }
}
