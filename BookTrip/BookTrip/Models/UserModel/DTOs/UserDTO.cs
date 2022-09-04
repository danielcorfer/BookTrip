namespace BookTrip.Models.UserModel.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Language { get; set; }
        public string Password { get; set; }
    }
}
