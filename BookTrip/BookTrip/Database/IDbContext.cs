using BookTrip.Models.HotelModel;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.ReviewModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.UserModel;
using Microsoft.EntityFrameworkCore;


namespace BookTrip.Database
{
    public interface IDbContext
    {
        DbSet<Hotel> Hotels { get; set; }
        DbSet<PropertyType> PropertyTypes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<UserSettings> UserSettings { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Bed> Beds { get; set; }
        DbSet<RoomReservation> RoomReservations { get; set; }
        DbSet<TaxiReservation> TaxiReservations { get; set; }
        IQueryable<T> Set<T>() where T : class;

        int SaveChanges();
    }
}
