using BookTrip.Interfaces;
using BookTrip.Models.General;
using BookTrip.Models.ReviewModel.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly ITokenService tokenService;

        public ReviewController(IReviewService reviewService, ITokenService tokenService)
        {
            this.reviewService = reviewService;
            this.tokenService = tokenService;
        }

        [HttpPost("")]
        [Authorize(Roles = "Guest,Admin")]
        public IActionResult Review([FromBody] ReviewDTO reviewDTO)
        {
            var loggedUser = tokenService.GetLoggedInUser();
            ResponseMessage response = reviewService.CreateReview(reviewDTO, loggedUser, out bool validation);
            if (validation)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
