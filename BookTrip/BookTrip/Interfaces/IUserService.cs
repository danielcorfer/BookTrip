using BookTrip.Models.General;
using BookTrip.Models.UserModel;
using BookTrip.Models.UserModel.DTOs;

namespace BookTrip.Interfaces
{
    public interface IUserService
    {
        User GetUserByEmail(string user);
        User GetUserById(Guid userId);
        void SetUserSettings(UserSettingsDTO settings, User user);
        ResponseMessage CreateNewUser(UserRegisterDTO user);
        void ChangeUICulture(string language);
        void ChangeCulture(string language);
        UserSettings GetUserLanguageSettings(UserSettingsDTO settings);
        ResponseMessage CurrentUserSettings();
        string GenerateRandomPassword();
        string ForgottenPassword(EmailDTO request);
        ResponseMessage EditUser(EditUserDTO input, User editedUser, out bool validRequest);
        bool IsUserEmailExists(string userEmail);
        bool IsEditFormValid(EditUserDTO input);

    }
}