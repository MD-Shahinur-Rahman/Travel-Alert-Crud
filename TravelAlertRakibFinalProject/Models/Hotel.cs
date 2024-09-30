namespace TravelAlertRakibFinalProject.Models
{
    public class Hotel
    {

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string HotelCode { get; set; }
        public decimal AverageRoomRate { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
        public ICollection<HotelImage> HotelImages { get; set; }
        public ICollection<HotelFacility> HotelFacilities { get; set; }
       

    }
}
