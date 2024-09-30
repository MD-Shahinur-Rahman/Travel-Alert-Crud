using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using TravelAlertRakibFinalProject.Dtos;
using TravelAlertRakibFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using TravelAlertRakibFinalProject.Models.DTO;

namespace TravelAlertRakibFinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RoomsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDtos>>> GetRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomImages)
                .Include(r => r.RoomFacilities)
                .ToListAsync();

            var roomsDtos = rooms.Select(r => new RoomDtos
            {
                RoomId = r.RoomId,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
                BedInRoom = r.BedInRoom,
                RoomNumber = r.RoomNumber,
                FloorNumber = r.FloorNumber,
                MaxOccupancy = r.MaxOccupancy,
                RoomStatus = r.RoomStatus,
                RoomImages = r.RoomImages,
                RoomFacilities = r.RoomFacilities
            });

            return Ok(roomsDtos);
        }
        private string GetUploadFile(RoomDtoimage dtoImage)
        {
            string uniqueFile = null;
            if (dtoImage.ImageProfile != null)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath, "Image");
                uniqueFile = Guid.NewGuid().ToString() + Path.GetExtension(dtoImage.ImageProfile.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFile);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dtoImage.ImageProfile.CopyTo(fileStream);
                }
            }
            return uniqueFile;
        }
        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDtos>> GetRoom(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .Include(r => r.RoomFacilities)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            var roomDtos = new RoomDtos
            {
                RoomId = room.RoomId,
                RoomType = room.RoomType,
                PricePerNight = room.PricePerNight,
                BedInRoom = room.BedInRoom,
                RoomNumber = room.RoomNumber,
                FloorNumber = room.FloorNumber,
                MaxOccupancy = room.MaxOccupancy,
                RoomStatus = room.RoomStatus,
                RoomImages = room.RoomImages,
                RoomFacilities = room.RoomFacilities
            };

            return Ok(roomDtos);
        }

        // POST: api/Rooms
        [HttpPost]
       // public async Task<ActionResult<RoomDtos>> CreateRoom([FromForm] RoomDto2 roomDto2)
        public async Task<ActionResult<RoomDtos>> CreateRoom([FromForm] TravelAlertRakibFinalProject.Models.DTO.RoomDto2 roomDto2)
        {
            var room = new Room
            {
                RoomType = roomDto2.RoomType,
                PricePerNight = roomDto2.PricePerNight,
                BedInRoom = roomDto2.BedInRoom,
                RoomNumber = roomDto2.RoomNumber,
                FloorNumber = roomDto2.FloorNumber,
                MaxOccupancy = roomDto2.MaxOccupancy,
                RoomStatus = roomDto2.RoomStatus,
                HotelId = roomDto2.HotelId
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            foreach (var image in roomDto2.RoomImages)
            {
                var roomImage = new RoomImage
                {
                    RoomId = room.RoomId,
                    ImageUrl = GetUploadFile(image),
                    ImageResolution = image.ImageResolution,
                    Caption = image.Caption,
                    IsThumbnail = image.IsThumbnail
                };

                _context.RoomImages.Add(roomImage);
            }

            foreach (var facility in roomDto2.RoomFacilities)
            {
                var roomFacility = new RoomFacility
                {
                    RoomId = room.RoomId,
                    FacilityId = facility.FacilityID
                };

                _context.RoomFacilities.Add(roomFacility);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, roomDto2);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDtos>> UpdateRoom(int id, [FromForm] TravelAlertRakibFinalProject.Models.DTO.RoomDto2 roomDto2)
        //public async Task<ActionResult<RoomDtos>> UpdateRoom(int id, [FromForm] RoomDto2 roomDto2)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .Include(r => r.RoomFacilities)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            room.RoomType = roomDto2.RoomType;
            room.PricePerNight = roomDto2.PricePerNight;
            room.BedInRoom = roomDto2.BedInRoom;
            room.RoomNumber = roomDto2.RoomNumber;
            room.FloorNumber = roomDto2.FloorNumber;
            room.MaxOccupancy = roomDto2.MaxOccupancy;
            room.RoomStatus = roomDto2.RoomStatus;

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            foreach (var image in roomDto2.RoomImages)
            {
                var roomImage = await _context.RoomImages
                    .FirstOrDefaultAsync(i => i.RoomId == id && i.ImageUrl == image.ImageUrl);

                if (roomImage != null)
                {
                    if (image.ImageProfile != null)
                    {
                        var uploadedFile = GetUploadFile(image);
                        roomImage.ImageUrl = uploadedFile;
                    }
                    roomImage.ImageResolution = image.ImageResolution;
                    roomImage.Caption = image.Caption;
                    roomImage.IsThumbnail = image.IsThumbnail;
                }
                else
                {
                    var uploadedFile = GetUploadFile(image);
                    roomImage = new RoomImage
                    {
                        RoomId = room.RoomId,
                        ImageUrl = uploadedFile,
                        ImageResolution = image.ImageResolution,
                        Caption = image.Caption,
                        IsThumbnail = image.IsThumbnail
                    };

                    _context.RoomImages.Add(roomImage);
                }
            }

            foreach (var facility in roomDto2.RoomFacilities)
            {
                var roomFacility = await _context.RoomFacilities
                    .FirstOrDefaultAsync(f => f.RoomId == id && f.FacilityId == facility.FacilityID);

                if (roomFacility != null)
                {
                    roomFacility.FacilityId = facility.FacilityID;
                }
                else
                {
                    roomFacility = new RoomFacility
                    {
                        RoomId = room.RoomId,
                        FacilityId = facility.FacilityID
                    };

                    _context.RoomFacilities.Add(roomFacility);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(roomDto2);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomDtos>> DeleteRoom(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .Include(r => r.RoomFacilities)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(new RoomDtos { RoomId = id });
        }
    }
}
