namespace TravelAlertRakibFinalProject.Models
{
    public class PackageAccommodation
    {
        public int PackageAccommodationId { get; set; }

        // Foreign Keys
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int PackageID { get; set; }
       // public Package Package { get; set; }

    }
}
