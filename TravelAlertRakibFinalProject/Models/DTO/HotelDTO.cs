﻿
using TravelAlertRakibFinalProject.Models;
using TravelAlertRakibFinalProject.Models.DTO;

namespace TravelAlertRakibFinalProject.Dtos
{
    public class HotelsDtos
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
        public ICollection<HotelImageDto> HotelImages { get; set; }
        public ICollection<HotelFacilityDto> HotelFacilities { get; set; }
    }
}

