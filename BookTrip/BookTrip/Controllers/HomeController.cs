using BookTrip.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookTrip.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHotelService hotelService;

        public HomeController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpGet("")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return LocalRedirect("~/swagger");
        }


        [HttpGet("api/Search/{city}/{capacity}")]
        public IActionResult SearchHotels(string city, int capacity)
        {
            return Ok(hotelService.Search(city, capacity));
        }
    }
}


