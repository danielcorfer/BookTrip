using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.ReviewModel;
using BookTrip.Models.UserModel;

namespace BookTrip.Models.HotelModel
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int PricePerNight { get; set; }
        public double StarRating { get; set; }
        public string TimeZoneId { get; set; }
        public PropertyType PropertyType { get; set; }
        public List<Review> Reviews { get; set; }

        public User? User { get; set; } = null;
        public List<Room> Rooms { get; set; }

        private Hotel()
        { }

        public Hotel(string name, string location, string country, string region, string city, string address, string description, int pricePerNight, PropertyType propertyType)
        {
            Name = name;
            Location = location;
            Country = country;
            Region = region;
            City = city;
            Address = address;
            Description = description;
            PricePerNight = pricePerNight;
            PropertyType = propertyType;
            Reviews = new List<Review>();
        }
    }
}
