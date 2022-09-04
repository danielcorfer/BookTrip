using BookTrip.Models.HotelModel;
using BookTrip.Models.UserModel;

namespace BookTrip.Models.ReviewModel
{
    public class Review
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
