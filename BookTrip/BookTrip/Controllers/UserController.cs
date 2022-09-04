using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.UserModel.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public UserController(IUserService userService, IEmailService emailService, ITokenService tokenService)
        {
            this.userService = userService;
            this.emailService = emailService;
            this.tokenService = tokenService;
        }

        [HttpPost("settings")]
        [Authorize]
        public IActionResult SetUserSettings([FromBody] UserSettingsDTO settings)
        {
            var loggedUser = tokenService.GetLoggedInUser();
            userService.SetUserSettings(settings, loggedUser);
            return Ok(userService.CurrentUserSettings());
        }

        [HttpPatch("edit")]
        [Authorize]
        public IActionResult EditUser([FromBody] EditUserDTO input)
        {
            bool validRequest;
            var editedUser = tokenService.GetLoggedInUser();
            ResponseMessage response = userService.EditUser(input, editedUser, out validRequest);
            if (validRequest)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("forgotten-password")]
        public IActionResult SendNewRandomPassword([FromBody] EmailDTO request)
        {
            bool isEmailSend;
            ResponseMessage response = emailService.SendEmailWithNewPassword(request, out isEmailSend);
            if (isEmailSend)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
