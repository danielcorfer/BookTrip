using BookTrip.Database;
using BookTrip.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using BookTrip.Models.General;
using BookTrip.Models.ReviewModel.DTOs;
using BookTrip.Models.ReviewModel;
using BookTrip.Models.UserModel;
using BookTrip.Models.HotelModel;

namespace BookTrip.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IDbContext database;
        private readonly IHotelService hotelService;
        private readonly IStringLocalizer<ReviewService> stringLocalizer;
        public ReviewService(IDbContext database, IHotelService hotelService, IStringLocalizer<ReviewService> stringLocalizer)
        {
            this.database = database;
            this.hotelService = hotelService;
            this.stringLocalizer = stringLocalizer;
        }

        public bool CheckIfUserWasInHotel(ReviewDTO reviewDTO, User user)
        {
            return user.RoomReservations.Any(rr => rr.Room.Hotel.Id == reviewDTO.HotelId);
        }

        public Review GetReviewByUserIdAndHotelId(Guid userId, int hotelId)
        {
            return database.Reviews.Include(r => r.User).Include(r => r.Hotel).FirstOrDefault(r => r.User.Id == userId && r.Hotel.Id == hotelId);
        }

        public ResponseMessage CreateReview(ReviewDTO reviewDTO, User user, out bool validation)
        {

            Review originalReview = GetReviewByUserIdAndHotelId(user.Id, reviewDTO.HotelId);

            if (CheckIfUserWasInHotel(reviewDTO, user))
            {
                if (originalReview != null)
                {
                    database.Reviews.Remove(originalReview);
                    Review newReview = new()
                    {
                        HotelId = reviewDTO.HotelId,
                        UserId = user.Id,
                        Score = reviewDTO.Score,
                        Text = reviewDTO.Text
                    };
                    database.Reviews.Add(newReview);
                    UpdateReviewScore(hotelService.GetHotelByHotelId(reviewDTO.HotelId));
                    database.SaveChanges();
                }

                validation = true;
                return new ResponseMessage
                {
                    Message = stringLocalizer["ReviewMessageTrue"]
                };
            }
            validation = false;
            return new ResponseMessage
            {
                Message = stringLocalizer["ReviewMessageFalse"]
            };

        }

        public void UpdateReviewScore(Hotel hotel)
        {
            hotel.StarRating = hotel.Reviews.Average(r => r.Score);
            database.SaveChanges();
        }

    }
}