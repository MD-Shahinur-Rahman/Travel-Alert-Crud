using Microsoft.EntityFrameworkCore;

namespace TravelAlertRakibFinalProject.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<HotelFacility> HotelFacilities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Facility> Facilities { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<PackageAccommodation> PackageAccommodations { get; set; }
     
      
        public DbSet<RoomFacility> RoomFacilities { get; set; }

      
        
    }
}
