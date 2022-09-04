using BookTrip.Models.HotelModel.RoomModel.DTOs;
using BookTrip.Models.ReviewModel.DTOs;

namespace BookTrip.Models.HotelModel.DTOs
{
    public class HotelDTO
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
        public string Type { get; set; }

        public List<ReviewDTO> Reviews { get; set; }
        public List<RoomDTO> Rooms { get; set; }

        private HotelDTO()
        { }

        public HotelDTO(int id, string name, string location, string country, string region, string city, string address, string description, int pricePerNight, int starRating, string propertyType)
        {
            Id = id;
            Name = name;
            Location = location;
            Country = country;
            Region = region;
            City = city;
            Address = address;
            Description = description;
            PricePerNight = pricePerNight;
            StarRating = starRating;
            Type = propertyType;
        }

        public HotelDTO(Hotel input)
        {
            Id = input.Id;
            Name = input.Name;
            Location = input.Location;
            Country = input.Country;
            Region = input.Region;
            City = input.City;
            Address = input.Address;
            Description = input.Description;
            PricePerNight = input.PricePerNight;
            StarRating = input.StarRating;
            Type = input.PropertyType.Type;
        }
    }
}
