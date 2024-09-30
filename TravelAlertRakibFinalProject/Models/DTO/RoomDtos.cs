using TravelAlertRakibFinalProject.Models;

namespace TravelAlertRakibFinalProject.Dtos
{
    public class RoomDtos
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int BedInRoom { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public string RoomStatus { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; }
    }
}
