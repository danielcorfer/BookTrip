using System.Globalization;
using System.Text;
using BookTrip.Database;
using BookTrip.Interfaces;
using BookTrip.Models;
using BookTrip.Models.General;
using BookTrip.Models.UserModel;
using BookTrip.Models.UserModel.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BookTrip.Services
{
    public class UserService : IUserService
    {
        private AppDbContext database;
        private readonly IStringLocalizer<UserService> stringLocalizer;

        public UserService(AppDbContext database, IStringLocalizer<UserService> stringLocalizer, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;
            this.stringLocalizer = stringLocalizer;
        }

        public User GetUserByEmail(string userEmail)
        {
            var searchedUser = database.Users.Include(u => u.Settings).SingleOrDefault(u => u.Email == userEmail);
            return searchedUser;
        }
        public User GetUserById(Guid userId)
        {
            var searchedUser = database.Users.Include(u => u.Settings).Include(u => u.RoomReservations).ThenInclude(rr => rr.Room).ThenInclude(r => r.Hotel).SingleOrDefault(u => u.Id == userId);
            return searchedUser;
        }

        public void SetUserSettings(UserSettingsDTO settings, User user)
        {
            var languageSettings = GetUserLanguageSettings(settings);
            user.Settings = new UserSettings();
            user.Settings.Language = languageSettings.Language;
            database.SaveChanges();
            ChangeUICulture(languageSettings.Language);
            ChangeCulture(languageSettings.Language);
        }

        //for text
        public void ChangeUICulture(string language)
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(language);
        }

        //for formatting
        public void ChangeCulture(string language)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(language);
        }

        public UserSettings GetUserLanguageSettings(UserSettingsDTO settings)
        {
            return database.UserSettings.FirstOrDefault(us => us.Language == settings.Language);
        }

        public ResponseMessage CurrentUserSettings()
        {
            return new ResponseMessage
            {
                Message = stringLocalizer["MessCurrLanguageSettings"]
            };
        }

        public ResponseMessage CreateNewUser(UserRegisterDTO user)
        {
            //there should be validation for language. Now it throws error 500 if the language is not valid.
            if (!Enum.TryParse<Enums.RoleEnum>(user.Role, out Enums.RoleEnum res))
            {
                return new ResponseMessage()
                {
                    Message = stringLocalizer["InvalidRole"]
                };

            }
            if (!(GetUserByEmail(user.Email) == null))
            {
                return new ResponseMessage()
                {
                    Message = stringLocalizer["UserExists"]
                };
            }

            User newUser = new User(user.Email, user.Password, user.Role)
            {
                Settings = new UserSettings()
                {
                    Language = user.Language
                }
            };
            database.Users.Add(newUser);
            database.SaveChanges();
            ChangeUICulture(newUser.Settings.Language);
            ChangeCulture(newUser.Settings.Language);
            return new ResponseMessage()
            {
                Message = stringLocalizer["UserRegistered"]
            };
        }

        public string GenerateRandomPassword()
        {
            int length = 10;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                sb.Append(valid[random.Next(valid.Length)]);
            }
            return sb.ToString();
        }

        public string ForgottenPassword(EmailDTO request)
        {
            var userByEmail = GetUserByEmail(request.UserEmail);
            if (userByEmail == null)
                return null;
            var newPassword = GenerateRandomPassword();
            userByEmail.Password = newPassword;
            database.SaveChanges();
            return newPassword;
        }

        public ResponseMessage EditUser(EditUserDTO input, User editedUser, out bool validRequest)
        {
            if (!IsEditFormValid(input))
            {
                validRequest = false;
                return new ResponseMessage
                {
                    Message = stringLocalizer["AccountNotChanged"]
                };
            }
            if (IsUserEmailExists(input.NewEmail))
            {
                validRequest = false;
                return new ResponseMessage
                {
                    Message = stringLocalizer["EmailExists"]
                };
            }
            if (!string.IsNullOrEmpty(input.NewEmail))
                editedUser.Email = input.NewEmail;
            if (!string.IsNullOrEmpty(input.NewPassword))
                editedUser.Password = input.NewPassword;
            if (!string.IsNullOrEmpty(input.NewRole))
                editedUser.Role = Enum.Parse<Enums.RoleEnum>(input.NewRole).ToString();
            database.SaveChanges();
            validRequest = true;
            return new ResponseMessage
            {
                Message = stringLocalizer["AccountChanged"]
            };
        }
        public bool IsUserEmailExists(string userEmail)
        {
            return database.Users.Where(u => u.Email == userEmail).Any();
        }
        public bool IsEditFormValid(EditUserDTO input)
        {
            return input.GetType().GetProperties().All(prop => prop.GetValue(input) != null);
        }
    }
}
