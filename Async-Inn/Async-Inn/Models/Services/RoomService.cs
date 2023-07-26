using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class RoomService : IRoom
	{
		private readonly AsyncInnDbContext _context;
		private readonly IRoomAmenity _roomamenity;	
		public RoomService(AsyncInnDbContext context, IRoomAmenity roomamenity) {
		 _context = context;
			_roomamenity=roomamenity;
		}

		public async Task<Room> AddAmenityToRoom(int roomId, int amenityId)
		{
			var AmenityRoom = await _context.RoomAmenities
				  .AddAsync(new RoomAmenity { RoomId = roomId, AmenityId = amenityId });
			    await _context.SaveChangesAsync();
			var room= await _context.Rooms.Include(x => x.Amenities).
				FirstOrDefaultAsync(x => x.Id == roomId);

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
			var room = await _context.Rooms.FindAsync(id);
			return room;
		}

		public async Task<List<Room>> GetRooms()
		{
			var rooms = await _context.Rooms.ToListAsync();
			return rooms;
		}

		public async Task<Room> RemoveAmentityFromRoom(int roomId, int amenityId)
		{
				var roomamenity = await _context.RoomAmenities
				.FindAsync(roomId,amenityId);
			    if(roomamenity != null)

			{
				 _context.RoomAmenities.Remove(roomamenity);
				await _context.SaveChangesAsync();
			}
			
			var room = await _context.Rooms.Include(x => x.Amenities).
				FirstOrDefaultAsync(x => x.Id == roomId);

			return room;
		}

		public async Task<Room> UpdateRoom(int id, Room room)
		{
			_context.Entry(room).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return room;
		}
	}
}
