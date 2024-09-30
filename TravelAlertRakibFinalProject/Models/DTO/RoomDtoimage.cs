namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class RoomDtoimage
    {
       
            public string? ImageUrl { get; set; }
            public IFormFile ImageProfile { get; set; }
            public string ImageResolution { get; set; }
            public string Caption { get; set; }
            public bool IsThumbnail { get; set; }
            public DateTime CreatedOn { get; set; }
        
    }
}
