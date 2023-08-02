using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _HotelRoom;

        public HotelRoomsController(IHotelRoom HotelRoom)

		{
			_HotelRoom = HotelRoom;
        }

        // GET: api/HotelRooms
        [Route("/api/Hotels/{hotelId}/Rooms")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms(int hotelId)
        {
          if (await _HotelRoom.GetHotelRooms(hotelId) == null)
          {
              return NotFound();
          }
            return await _HotelRoom.GetHotelRooms(hotelId);
        }

        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [HttpGet]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
          if (await _HotelRoom.GetHotelRoom(hotelId,roomNumber)== null)
          {
              return NotFound();
          }
            var hotelRoom =await _HotelRoom.GetHotelRoom(hotelId, roomNumber);


            return hotelRoom;

		}

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [HttpPut]
        public async Task<IActionResult> PutHotelRoom(int hotelId,int roomNumber, HotelRoomDTO hotelRoom)
        {

            var hotelRoomupdated = await _HotelRoom.UpdateHotelRoom(hotelId, roomNumber, hotelRoom);
            if (hotelRoomupdated==null)
            {
				return NoContent();
			}

			return CreatedAtAction("GetHotelRoom", new { HotelId=hotelId,RoomNumber=roomNumber }, hotelRoomupdated);


		}

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/api/Hotels/{hotelId}/Rooms")]
        [HttpPost]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoomDTO hotelRoom,int hotelId)
        {
            hotelRoom.HotelID=hotelId;  
            var hotelRoomAdded = await _HotelRoom.Create(hotelRoom,hotelId);
          if (hotelRoomAdded == null)
          {
              return Problem("Entity set 'AsyncInnDbContext.HotelRooms'  is null.");
          }
           
            

            return CreatedAtAction("GetHotelRoom", new { hotelRoomAdded.HotelID, hotelRoomAdded.RoomNumber }, hotelRoomAdded);
        }

        // DELETE: api/HotelRooms/5
        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
			await _HotelRoom.Delete( hotelId,  roomNumber);

			return NoContent();
		}

    }
}
