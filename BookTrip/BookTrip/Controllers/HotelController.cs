using BookTrip.Interfaces;
using BookTrip.Models.HotelModel.DTOs;
using BookTrip.Models.HotelModel.RoomModel.DTOs;
using BookTrip.Models.UserModel.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly IRoomService roomService;
        private readonly ITokenService tokenService;

        public HotelController(IHotelService hotelService, IRoomService roomService, ITokenService tokenService)
        {
            this.hotelService = hotelService;
            this.roomService = roomService;
            this.tokenService = tokenService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult AddNewHotel([FromBody] UserHotelDTO input)
        {
            var currentUser = tokenService.GetLoggedInUser();
            return Ok(hotelService.AddNewHotel(input, currentUser));
        }

        [HttpGet("list")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult ListHotels()
        {
            var currentUser = tokenService.GetLoggedInUser();
            return Ok(hotelService.ListHotels(currentUser.Id));
        }

        [HttpPut("{hotelId}")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult EditHotelDetails([FromRoute] string hotelId, [FromBody]EditHotelDTO input)
        {
            return Ok(hotelService.EditHotelDetails(hotelId, input));
        }

        [HttpPost("addroom")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult AddNewRoom([FromBody] RoomDTO input)
        {
            var currentUser = tokenService.GetLoggedInUser();
            roomService.AddOneRoom(input);
            return Ok();
        }

        [HttpPost("addmultipleroom")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult AddMultipleRooms([FromBody] RoomMultipleDTO input)
        {
            var currentUser = tokenService.GetLoggedInUser();
            roomService.AddMultipleBedsToRoom(input);
            return Ok();
        }
    }
}
