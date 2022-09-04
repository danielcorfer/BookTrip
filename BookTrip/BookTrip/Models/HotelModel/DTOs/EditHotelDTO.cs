namespace BookTrip.Models.HotelModel.DTOs
{
    public class EditHotelDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int PricePerNight { get; set; }
        public string TimeZone { get; set; }

        private EditHotelDTO()
        { }

        public EditHotelDTO(int id, string name, string location, string country, string region, string city, string address, string description, int pricePerNight, int starRating, string propertyType)
        {
            Name = name;
            Location = location;
            Country = country;
            Region = region;
            City = city;
            Address = address;
            Description = description;
            PricePerNight = pricePerNight;
        }
    }
}
