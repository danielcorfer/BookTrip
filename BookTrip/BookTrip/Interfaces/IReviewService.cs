using BookTrip.Models.General;
using BookTrip.Models.ReviewModel;
using BookTrip.Models.ReviewModel.DTOs;
using BookTrip.Models.UserModel;

namespace BookTrip.Interfaces
{
    public interface IReviewService
    {
        bool CheckIfUserWasInHotel(ReviewDTO input, User user);
        public ResponseMessage CreateReview(ReviewDTO reviewDTO, User user, out bool validation);
        Review GetReviewByUserIdAndHotelId(Guid userId, int hotelId);
    }
}
