using BookTrip.Interfaces;
using BookTrip.Services;
using Microsoft.Extensions.Localization;
using Moq;
using System.Text.Json;
using BookTrip.Database;
using NPOI.SS.Formula.Functions;
using AutoMapper;
using MoQUnitTests;
using BookTrip.Models.HotelModel.DTOs;
using BookTrip.Models.HotelModel;

namespace UnitTests
{
    public class HotelServiceTests
    {
        //Creating fields
        private HotelService _hotelService;
        private readonly Mock<IDbContext> _mockAppDbContext = new Mock<IDbContext>();
        private readonly Mock<IPropertyTypeService> _mockPropertyService = new Mock<IPropertyTypeService>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IStringLocalizer<HotelService>> _mockStringLocalizer = new Mock<IStringLocalizer<HotelService>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        //Testing Database Related stuff
        public HotelServiceTests()
        {
            _hotelService = (new Mock<HotelService>(_mockAppDbContext.Object, _mockPropertyService.Object, _mockUserService.Object, _mockStringLocalizer.Object, _mockMapper.Object)).Object;
        }

        [Fact]
        public void HotelDetailsReturnCorrectJson()
        {
            //arrange
            var data = new List<Hotel> { new Hotel("test", "test", "test", "test", "test", "test", "test", 1, new PropertyType("test")) { Id = 1 , TimeZoneId = TimeZoneInfo.Local.Id} };
            int id = 1;

            _mockAppDbContext.Setup(db => db.Hotels).Returns(MoqSetup.SetupMockSet(data.AsQueryable()).Object);

            var expected = JsonSerializer.Serialize(new HotelDTO(data[0]));
            var actual = JsonSerializer.Serialize(_hotelService.HotelDetailsMessage(id));

            Assert.Equal(expected, actual);
        }
    }
}