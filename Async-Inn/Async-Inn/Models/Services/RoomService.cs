using System.Reflection;
using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Async_Inn.Models.Services
{
	public class RoomService : IRoom
	{
		private readonly AsyncInnDbContext _context;
		public RoomService(AsyncInnDbContext context) {
		 _context = context;
		}

		public async Task<Room> AddAmenityToRoom(int roomId, int amenityId)
		{
			var room = await GetRoom(roomId);
			var amenity = await _context.Amenities.FindAsync(amenityId);
			if(room != null && amenity !=null)
			{
				await _context.RoomAmenities.AddAsync(new RoomAmenity{RoomId=roomId, AmenityId=amenityId});
				await _context.SaveChangesAsync();
			}
			return room;
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
			var room = await _context.Rooms
				.Include(r=> r.roomAmenities)
				.FirstOrDefaultAsync(r=> r.Id==id);
			return room;
		}

		public async Task<List<Room>> GetRooms()
		{
			var rooms = await _context.Rooms
				.Include(r => r.roomAmenities).ToListAsync();
			return rooms;
		}

		public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
		{
			var RoomAminity = await _context.RoomAmenities.FindAsync(roomId, amenityId);
			if(RoomAminity != null) {
				 _context.RoomAmenities.Remove(RoomAminity);
				await _context.SaveChangesAsync();	
			}
		}

		public async Task<Room> UpdateRoom(int id, Room room)
		{
			_context.Entry(room).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return room;
		}
	}
}
