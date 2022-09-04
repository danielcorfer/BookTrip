using BookTrip.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using BookTrip.Models.HotelModel;

namespace MoQIntegrationTests
{
    public class IntegrationTestsWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));
                services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using(var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();

                    db.Database.EnsureCreated();

                    try
                    {
                        if (!db.Hotels.Any())
                        {
                            for (int i = 1; i < 10; i++)
                            {
                                db.Hotels.Add(new Hotel($"testing{i}", $"testing{i}", $"testing{i}", $"testing{i}", $"testing{i}", $"testing{i}", $"testing{i}", i, new PropertyType($"testing{i}")) { TimeZoneId = TimeZoneInfo.Local.Id});
                            }
                        }
                        db.SaveChanges();
                    }
                    catch(Exception ex)
                    {

                    };
                }
            });
        }
    }
}
