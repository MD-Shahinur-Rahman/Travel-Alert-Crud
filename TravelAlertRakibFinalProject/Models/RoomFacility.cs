namespace TravelAlertRakibFinalProject.Models
{
    public class RoomFacility
    {
        public int RoomFacilityId { get; set; }

        // Foreign Keys
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int FacilityId { get; set; }
        public Facility Facility { get; set; }

        // Timestamps
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
