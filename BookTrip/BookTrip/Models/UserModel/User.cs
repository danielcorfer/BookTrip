using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using BookTrip.Models.HotelModel;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.ReviewModel;

namespace BookTrip.Models.UserModel
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Email { get; set; }
        private string _password;
        public string Password
        {
            get => _password;
            set { _password = HashPassword(value); }
        }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Review> Reviews { get; set; }
        [ForeignKey("UserSettings")]
        public UserSettings Settings { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
       
        public User()
        {

        }

        public User(string email, string password, Enums.RoleEnum role)
        {
            Email = email;
            Role = role.ToString();
            PasswordSalt = RandomNumberGenerator.GetBytes(16);
            Password = password;
            Hotels = new List<Hotel>();
            Settings = new UserSettings();
            RoomReservations = new List<RoomReservation>();
        }
        public User(string email, string password, string role)
        {
            Email = email;
            Role = role;
            PasswordSalt = RandomNumberGenerator.GetBytes(16);
            Password = password;
            Hotels = new List<Hotel>();
            Settings = new UserSettings();
            RoomReservations = new List<RoomReservation>();
        }

        private string HashPassword(string passwordInput)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(passwordInput, PasswordSalt, 87640);
            byte[] hash = pbkdf2.GetBytes(20);

            var result = Convert.ToBase64String(hash);
            return result;
        }

        public bool PasswordCheck(string passwordInput)
        {
            return HashPassword(passwordInput) == Password;
        }
    }
}
