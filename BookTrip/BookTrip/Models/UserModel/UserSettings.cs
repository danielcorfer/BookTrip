namespace BookTrip.Models.UserModel
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public User User { get; set; }

        public UserSettings()
        {

        }

        public UserSettings(string language, User user)
        {
            Language = language;
            User = user;
        }
    }
}
