namespace TravelAlertRakibFinalProject.Models
{
    public class Room
    {

        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int BedInRoom { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public string RoomStatus { get; set; } // Consider using enum for predefined statuses

        // Foreign Key
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        // Relationships
        public ICollection<RoomImage> RoomImages { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; } // Room <-> Facility many-to-many
    }
}
