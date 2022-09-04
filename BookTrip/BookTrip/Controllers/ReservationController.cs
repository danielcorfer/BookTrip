using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.ReservationModel.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly IRoomReservationService roomReservationService;
        private readonly IEmailService emailService;

        public ReservationController(ITokenService tokenService, IRoomReservationService roomReservationService, IEmailService emailService)
        {
            this.tokenService = tokenService;
            this.roomReservationService = roomReservationService;
            this.emailService = emailService;
        }

        [HttpPost("")]
        [Authorize(Roles = "Guest")]
        public IActionResult RoomReservation([FromBody] RoomReservationDTO request)
        {
            bool isReservationRegistered;
            var loggedInUser = tokenService.GetLoggedInUser();
            ResponseMessage response = roomReservationService.BookRoom(request, loggedInUser, out isReservationRegistered, out RoomReservation roomReservation);
            if (isReservationRegistered)
            {
                emailService.SendReservationConfirmationEmail(roomReservation);
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{deletionToken}/{roomReservationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromRoute] string deletionToken, int roomReservationId)
        {
            ResponseMessage responseMessage = roomReservationService.DeleteRoomReservation(deletionToken, roomReservationId, out bool deletionSuccessful);
            if (deletionSuccessful)
            {
                return Ok(responseMessage);
            }
            return BadRequest(responseMessage);
        }
    }
}
