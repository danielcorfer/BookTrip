namespace BookTrip.Models.UserModel.DTOs
{
    public class UserHotelDTO
    {
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int PricePerNight { get; set; }
        public int StarRating { get; set; }
        public string PropertyType { get; set; }
    }
}
