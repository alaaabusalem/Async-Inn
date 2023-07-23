using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class RoomService : IRoom
	{
		private readonly AsyncInnDbContext _context;
		public RoomService(AsyncInnDbContext context) {
		 _context = context;
		}	
		public async Task<Room> Create(Room room)
		{
		 	await _context.AddAsync(room);
			await _context.SaveChangesAsync();
			return room;
		}

		public async Task Delete(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if(room != null)
			{
				 _context.Rooms.Remove(room);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<Room> GetRoom(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			return room;
		}

		public async Task<List<Room>> GetRooms()
		{
			var rooms = await _context.Rooms.ToListAsync();
			return rooms;
		}

		public async Task<Room> UpdateRoom(int id, Room room)
		{
			var roomToupdate = await _context.Rooms.FindAsync(room.Id);

			if(roomToupdate != null)
			{
				_context.Rooms.Update(room);
				await _context.SaveChangesAsync();return room;
			}
			return null;
		}
	}
}
