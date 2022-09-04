using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using BookTrip.Models.HotelModel.DTOs;

namespace MoQIntegrationTests
{
    public class HotelControllerTests :
        IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
    {
        //Creating fields

        private readonly HttpClient _client;
        private readonly IntegrationTestsWebApplicationFactory<Program> _factory;

        //Testing Database Related stuff
        public HotelControllerTests(IntegrationTestsWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }

        [Fact]
        public async Task GetDetailsStatusCodeAndSendsCorrectInformation()
        {
            var actualResponse = await _client.GetAsync("/api/hotel/4");

            var content = await actualResponse.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<HotelDTO>(content);


            actualResponse.EnsureSuccessStatusCode();
            Assert.Equal("testing4", responseObject.Name);
        }

        [Fact]
        public async Task AddNewHotelPageNotLoadsSuccesfully()
        {
            var actualResponse = await _client.GetAsync("/api/hotel/add");
            var content = actualResponse.StatusCode;
            var expectedStatusCode = 404;

            Assert.Equal((int)expectedStatusCode, (int)content);
        }

        [Fact]
        public async Task SearchHotelsLoadsSuccesfully()
        {
            var actualResponse = await _client.GetAsync("/api/search/testing4/1");
            //var content = actualResponse.Content.ReadAsStreamAsync().Result;

            var content = actualResponse.Content.ReadAsStringAsync()
                                                .Result;
            var responseObject = JsonConvert.DeserializeObject<HotelDTO[]>(content);

            actualResponse.EnsureSuccessStatusCode();
            Assert.Equal("testing4", responseObject[0].City);
        }
    }
}
