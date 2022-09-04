using BookTrip.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookTrip.Models.General;
using BookTrip.Models.TaxiReservationModel.DTOs;
using BookTrip.Models.TaxiReservationModel;

namespace BookTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {

        private readonly ITokenService tokenService;
        private readonly ITaxiService taxiService;
        private readonly IEmailService emailService;

        public TaxiController(ITokenService tokenService, ITaxiService taxiService, IEmailService emailService)
        {

            this.tokenService = tokenService;
            this.taxiService = taxiService;
            this.emailService = emailService;
        }
        [HttpPost("")]
        [Authorize(Roles = "Guest")]

        public async Task<IActionResult> Post([FromBody] TaxiReservationDTO taxiDTO)
        {
            var user = tokenService.GetLoggedInUser();
            ResponseMessage responseMessage = taxiService.CreateReservation(taxiDTO, user, out bool isReservationRegistered, out TaxiReservation taxiReservation);

            if (isReservationRegistered)
            {
                emailService.SendEmailWithDeletionLink(taxiReservation);
                return Ok(responseMessage);
            }
            return BadRequest(responseMessage);
        }

        [HttpGet("{deletionToken}/{taxiReservationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromRoute] string deletionToken, int taxiReservationId)
        {
            ResponseMessage responseMessage = taxiService.DeleteReservation(deletionToken, taxiReservationId, out bool deletionSuccessful);
            if (deletionSuccessful)
            {
                return Ok(responseMessage);
            }

            return BadRequest(responseMessage);
        }
    }
}
