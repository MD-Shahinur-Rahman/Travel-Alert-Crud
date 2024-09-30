using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelAlertRakibFinalProject.Models;
using TravelAlertRakibFinalProject.Dtos;
using System.IO;
using System.Threading.Tasks;
using TravelAlertRakibFinalProject.Models.DTO;

namespace TravelAlertRakibFinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HotelsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelsDtos>>> GetHotels()
        {
            var hotels = await _context.Hotels
                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .ToListAsync();

            var hotelsDtos = hotels.Select(h => new HotelsDtos
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                Description = h.Description,
                StarRating = h.StarRating,
                Address = h.Address,
                ContactInfo = h.ContactInfo,
                HotelCode = h.HotelCode,
                AverageRoomRate = h.AverageRoomRate,
                LocationID = h.LocationID,
                HotelImages = h.HotelImages.Select(i => new HotelImageDto
                {
                    HotelImageId = i.HotelImageId,
                    ImageUrl = i.ImageUrl,
                    ImageResolution = i.ImageResolution,
                    Caption = i.Caption,
                    IsThumbnail = i.IsThumbnail
                }).ToList(),
                HotelFacilities = h.HotelFacilities.Select(f => new HotelFacilityDto
                {
                    
                    FacilityID = f.FacilityID,
                    
                }).ToList()
            });

            return Ok(hotelsDtos);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelsDtos>> GetHotel(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = new HotelsDtos
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                Description = hotel.Description,
                StarRating = hotel.StarRating,
                Address = hotel.Address,
                ContactInfo = hotel.ContactInfo,
                HotelCode = hotel.HotelCode,
                AverageRoomRate = hotel.AverageRoomRate,
                LocationID = hotel.LocationID,
                HotelImages = hotel.HotelImages.Select(i => new HotelImageDto
                {
                    HotelImageId = i.HotelImageId,
                    ImageUrl = i.ImageUrl,
                    ImageResolution = i.ImageResolution,
                    Caption = i.Caption,
                    IsThumbnail = i.IsThumbnail
                }).ToList(),
                HotelFacilities = hotel.HotelFacilities.Select(f => new HotelFacilityDto
                {
                   
                    FacilityID = f.FacilityID,
                  
                }).ToList()
            };

            return Ok(hotelDto);
        }

        // POST: api/Hotels
        [HttpPost]
        public async Task<ActionResult<HotelsDtos>> CreateHotel(HotelsDtos hotelDto)
        {
            var hotel = new Hotel
            {
                HotelName = hotelDto.HotelName,
                Description = hotelDto.Description,
                StarRating = hotelDto.StarRating,
                Address = hotelDto.Address,
                ContactInfo = hotelDto.ContactInfo,
                HotelCode = hotelDto.HotelCode,
                AverageRoomRate = hotelDto.AverageRoomRate,
                LocationID = hotelDto.LocationID
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            foreach (var image in hotelDto.HotelImages)
            {
                var hotelImage = new HotelImage
                {
                    HotelId = hotel.HotelId,
                    ImageUrl = image.ImageUrl,
                    ImageResolution = image.ImageResolution,
                    Caption = image.Caption,
                    IsThumbnail = image.IsThumbnail
                };

                _context.HotelImages.Add(hotelImage);
                await _context.SaveChangesAsync();
            }

            foreach (var facility in hotelDto.HotelFacilities)
            {
                var hotelFacility = new HotelFacility
                {
                    HotelId = hotel.HotelId,
                    FacilityID = facility.FacilityID,
                   
                };

                _context.HotelFacilities.Add(hotelFacility);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.HotelId }, hotelDto);
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        public async Task<ActionResult<HotelsDtos>> UpdateHotel(int id, HotelsDtos hotelDto)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.HotelName = hotelDto.HotelName;
            hotel.Description = hotelDto.Description;
            hotel.StarRating = hotelDto.StarRating;
            hotel.Address = hotelDto.Address;
            hotel.ContactInfo = hotelDto.ContactInfo;
            hotel.HotelCode = hotelDto.HotelCode;
            hotel.AverageRoomRate = hotelDto.AverageRoomRate;
            hotel.LocationID = hotelDto.LocationID;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            foreach (var image in hotelDto.HotelImages)
            {
                var hotelImage = await _context.HotelImages
                    .FirstOrDefaultAsync(i => i.HotelImageId == image.HotelImageId);

                if (hotelImage == null)
                {
                    hotelImage = new HotelImage
                    {
                        HotelId = hotel.HotelId,
                        ImageUrl = image.ImageUrl,
                        ImageResolution = image.ImageResolution,
                        Caption = image.Caption,
                        IsThumbnail = image.IsThumbnail
                    };

                    _context.HotelImages.Add(hotelImage);
                }
                else
                {
                    hotelImage.ImageUrl = image.ImageUrl;
                    hotelImage.ImageResolution = image.ImageResolution;
                    hotelImage.Caption = image.Caption;
                    hotelImage.IsThumbnail = image.IsThumbnail;

                    _context.HotelImages.Update(hotelImage);
                }

                await _context.SaveChangesAsync();
            }

            foreach (var facility in hotelDto.HotelFacilities)
            {
                var hotelFacility = await _context.HotelFacilities
                    .FirstOrDefaultAsync(f => f.HotelFacilityId == facility.);

                if (hotelFacility == null)
                {
                    hotelFacility = new HotelFacility
                    {
                        HotelId = hotel.HotelId,
                        FacilityID = facility.FacilityID,
                     
                    };

                    _context.HotelFacilities.Add(hotelFacility);
                }
                else
                {
                    hotelFacility.FacilityID = facility.FacilityID;
                  

                    _context.HotelFacilities.Update(hotelFacility);
                }

                await _context.SaveChangesAsync();
            }

            return Ok(hotelDto);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelsDtos>> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelImages)
                .Include(h => h.HotelFacilities)
                .FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Hotels/5/Images
        [HttpPost("{id}/Images")]
        public async Task<ActionResult<HotelImageDto>> UploadImage(int id, IFormFile image)
        {
            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelImage = new HotelImage
            {
                HotelId = hotel.HotelId,
                ImageUrl = await UploadFile(image)
            };

            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.HotelId }, new HotelImageDto
            {
                HotelImageId = hotelImage.HotelImageId,
                ImageUrl = hotelImage.ImageUrl
            });
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}