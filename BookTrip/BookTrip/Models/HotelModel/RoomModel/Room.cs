using BookTrip.Models.HotelModel.BedTypes;
using BookTrip.Models.ReservationModel;

namespace BookTrip.Models.HotelModel.RoomModel
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null;
        public int Price { get; set; }
        public int? NumberOfGuestsLivingIn { get; set; } = null;
        public bool IsBooked { get; set; } = false;
        public bool IsCheckedIn { get; set; } = false;
        public Hotel Hotel { get; set; }
        public List<RoomBed> RoomBeds { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }

        private Room()
        {
        }

        public Room(int price, Hotel hotel, string roomName = null)
        {
            RoomName = roomName;
            Price = price;
            Hotel = hotel;
            RoomBeds = new List<RoomBed>();
            RoomReservations = new List<RoomReservation>();
        }
        public Room(int price, Hotel hotel, int numberOfBeds, string[] bedName, string roomName = null, int customBedSleepingSpots = 0)
        {
            RoomName = roomName;
            Price = price;
            Hotel = hotel;
            RoomBeds = new List<RoomBed>();
            RoomReservations = new List<RoomReservation>();
            var bedTypes = bedName.ToList();
            for (int i = 0; i < numberOfBeds; i++)
            {
                switch (bedTypes[i])
                {
                    case "fullbed":
                        var numberOfFullBed = bedTypes.Count(i => i == "fullbed");
                        RoomBeds.Add(new RoomBed(this, new FullBed(), numberOfFullBed));
                        bedTypes.RemoveAll(i => i == "fullbed");
                        break;
                    case "singlebed":
                        var numberOfSingleBed = bedTypes.Count(i => i == "singlebed");
                        RoomBeds.Add(new RoomBed(this, new SingleBed(), numberOfSingleBed));
                        bedTypes.RemoveAll(i => i == "singlebed");
                        break;
                    case "SofaBed":
                        var numberOfSofaBed = bedTypes.Count(i => i == "sofabed");
                        RoomBeds.Add(new RoomBed(this, new SofaBed(), numberOfSofaBed));
                        bedTypes.RemoveAll(i => i == "sofabed");
                        break;
                    case "twinbed":
                        var numberOfTwinBed = bedTypes.Count(i => i == "twinbed");
                        RoomBeds.Add(new RoomBed(this, new TwinBed(), numberOfTwinBed));
                        bedTypes.RemoveAll(i => i == "twinbed");
                        break;
                    default:
                        CreateCustomBedsFromParams(bedTypes, bedTypes[i], customBedSleepingSpots);
                        break;
                }
            }
        }

        private void CreateCustomBedsFromParams(List<string> list, string nameOfCustomBed, int customBedSleepingSpots)
        {
            var numberOfCustomBed = list.Count(i => i == nameOfCustomBed);
            RoomBeds.Add(new RoomBed(this, new CustomBed() { Type = nameOfCustomBed, NumberOfSleepingSpots = customBedSleepingSpots }, numberOfCustomBed));
            list.RemoveAll(i => i == nameOfCustomBed);
        }
    }
}
