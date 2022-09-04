using BookTrip.Models.UserModel;

namespace BookTrip.Interfaces
{
    public interface ITokenService
    {
        string CreateLoginToken(User user);
        string CreateEmailToken(int taxiReservationId, DateTime departure);
        public string CreateEmailToken(int roomReservationId);
        User GetLoggedInUser();
    }
}
