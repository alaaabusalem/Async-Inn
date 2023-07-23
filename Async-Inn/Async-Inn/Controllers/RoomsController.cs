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

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _Room;

        public RoomsController(IRoom room)
        {
			_Room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var Rooms = await _Room.GetRooms();
			if (Rooms == null)
			{
				return NotFound();
			}
			return Rooms;
		}

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
			var room = await _Room.GetRoom(id);
			if (room == null)
			{
				return NotFound();
			}

			return room;
		}

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
			if (id != room.Id)
			{
				return BadRequest();
			}
			var updatedRoom = await _Room.UpdateRoom(id, room);
			return Ok(updatedRoom);


		}

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
			await _Room.Create(room);

			// Rurtn a 201 Header to Browser or the postmane
			return CreatedAtAction("GetRoom", new { id = room.Id }, room);
		}

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
			await _Room.Delete(id);

			return NoContent();
		}

      
    }
}
