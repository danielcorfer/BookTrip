using BookTrip.Models.HotelModel;
using BookTrip.Models.HotelModel.BedTypes;
using BookTrip.Models.HotelModel.RoomModel;
using BookTrip.Models.ReservationModel;
using BookTrip.Models.ReviewModel;
using BookTrip.Models.TaxiReservationModel;
using BookTrip.Models.UserModel;
using Microsoft.EntityFrameworkCore;

namespace BookTrip.Database
{
    public class AppDbContext : DbContext, IDbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<TaxiReservation> TaxiReservations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        IQueryable<T> IDbContext.Set<T>()
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Hotel
            modelBuilder.Entity<Hotel>().HasKey(h => h.Id);

            //PropertyType
            modelBuilder.Entity<PropertyType>().HasKey(pt => pt.Type);

            //User
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            //Review
            modelBuilder.Entity<Review>().HasKey(u => u.Id);
            //UserSettings
            modelBuilder.Entity<UserSettings>().HasKey(us => us.Id);

            //Room
            modelBuilder.Entity<Room>().HasKey(r => r.RoomId);

            //Bed
            modelBuilder.Entity<Bed>().HasKey(b => b.Id);
            modelBuilder.Entity<Bed>().HasDiscriminator<string>("Bed Type")
                           .HasValue<FullBed>("Full Bed")
                           .HasValue<TwinBed>("Twin Bed")
                           .HasValue<SingleBed>("Single Bed")
                           .HasValue<SofaBed>("Sofa Bed")
                           .HasValue<CustomBed>("Custom Bed");
            //RoomReservation
            modelBuilder.Entity<RoomReservation>().HasKey(rr => rr.Id);
            //TaxiReservation
            modelBuilder.Entity<RoomReservation>().HasKey(tr => tr.Id);

            //Table Relationships
            modelBuilder.Entity<Hotel>().HasOne(h => h.PropertyType).WithMany(pt => pt.Hotels);
            modelBuilder.Entity<User>().HasMany(u => u.Hotels).WithOne(h => h.User);
            modelBuilder.Entity<User>().HasOne(u => u.Settings).WithOne(us => us.User);
            modelBuilder.Entity<User>().HasMany(u => u.Reviews).WithOne(r => r.User);
            modelBuilder.Entity<Hotel>().HasMany(h => h.Reviews).WithOne(r => r.Hotel);
            modelBuilder.Entity<RoomReservation>().HasOne(rr => rr.Room).WithMany(r => r.RoomReservations);
            modelBuilder.Entity<RoomReservation>().HasMany(rr => rr.TaxiReservations).WithOne(tr => tr.RoomReservation);
            modelBuilder.Entity<User>().HasMany(u => u.RoomReservations).WithOne(r => r.User);
            modelBuilder.Entity<Hotel>().HasMany(h => h.Rooms).WithOne(r => r.Hotel);
        }
    }
}