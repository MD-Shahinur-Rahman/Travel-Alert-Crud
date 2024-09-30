namespace TravelAlertRakibFinalProject.Models
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageResolution { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; } // Corrected typo
        public DateTime CreatedOn { get; set; }

        // Foreign Key
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
