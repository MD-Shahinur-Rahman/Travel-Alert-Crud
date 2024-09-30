namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class RoomDto2
    {
        
            public int RoomId { get; set; }
            public string RoomType { get; set; }
            public decimal PricePerNight { get; set; }
            public int BedInRoom { get; set; }
            public int RoomNumber { get; set; }
            public int FloorNumber { get; set; }
            public int MaxOccupancy { get; set; }
            public string RoomStatus { get; set; }
            public int HotelId { get; set; }
            public ICollection<RoomDtoimage> RoomImages { get; set; }
            public ICollection<RoomFacilityDTOs> RoomFacilities { get; set; }
        
    }
}
