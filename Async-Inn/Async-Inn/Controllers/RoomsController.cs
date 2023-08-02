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
    public class RoomsController : ControllerBase
    {
		private readonly IRoom _Room;

		public RoomsController(IRoom room)
		{
			_Room = room;
		}

		// GET: api/Rooms
		[HttpGet]
		public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
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
		public async Task<ActionResult<RoomDTO>> GetRoom(int id)
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
		public async Task<IActionResult> PutRoom(int id, RoomDTO room)
		{
			if (id != room.ID)
			{
				return BadRequest();
			}
			var updatedRoom = await _Room.UpdateRoom(id, room);
			return Ok(updatedRoom);


		}

		// POST: api/Rooms
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Room>> PostRoom(RoomDTO room)
		{
			await _Room.Create(room);

			// Rurtn a 201 Header to Browser or the postmane
			return CreatedAtAction("GetRoom", new { id = room.ID }, room);
		}

		[Route("{roomId}/Amenity/{amenityId}")]
		[HttpPost]
		public async Task<ActionResult<RoomDTO>> PostRoomAmenity(int roomId, int amenityId)
		{
			var room = await _Room.AddAmenityToRoom(roomId, amenityId);

			// Rurtn a 201 Header to Browser or the postmane
			return CreatedAtAction("GetRoom", new { id = room.ID }, room);

		}
		// DELETE: api/Rooms/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRoom(int id)
		{
			await _Room.Delete(id);

			return NoContent();
		}

		[Route("{roomId}/Amenity/{amenityId}")]

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveAmentityFromRoom(int roomId, int amenityId)
		{
			await _Room.RemoveAmentityFromRoom(roomId, amenityId);

			return NoContent();
		}
	}
}
