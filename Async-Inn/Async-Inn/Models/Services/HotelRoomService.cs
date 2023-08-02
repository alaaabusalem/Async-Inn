using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class HotelRoomService : IHotelRoom
	{
		private readonly AsyncInnDbContext _context;
		public HotelRoomService(AsyncInnDbContext context) {
		
		_context = context;
		}	
		public async Task<HotelRoom> Create(HotelRoom hotelRoom, int HotelId	)
		{
			var Hotel = await _context.Hotels.FindAsync(hotelRoom.HotelId);
			//var Room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
			if (Hotel != null)
			{
				await _context.HotelRooms.AddAsync(hotelRoom);
				await _context.SaveChangesAsync();
				return hotelRoom;
			}
			return null;
			//_context.Entry(hotelRoom).State = EntityState.Added;
			//await _context.SaveChangesAsync();
			//return hotelRoom;	

		}

		public async Task Delete(int hotelId, int roomNumber)
		{
			var hotelroom = await _context.HotelRooms
				.FirstOrDefaultAsync(HR=>HR.HotelId==hotelId && HR.RoomNumber== roomNumber);
			//var hotelroom = await _context.HotelRooms.GetById(hotelId, roomNumber);
			if (hotelroom != null)
			{
				_context.HotelRooms.Remove(hotelroom);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
		{
			var Room = await _context.HotelRooms
				
				.FirstOrDefaultAsync(HR => HR.HotelId == hotelId && HR.RoomNumber == roomNumber);
			return Room;
		}

		public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
		{
			var Rooms = await _context.HotelRooms.Where(HR=> HR.HotelId==hotelId).ToListAsync();
			return Rooms;
		}

		public async Task<HotelRoom> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
		{
			var Hotel = await _context.Hotels.FindAsync(hotelRoom.HotelId);
			var Room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
			if(Hotel == null || Room==null) { return null; }
			_context.Entry(hotelRoom).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return hotelRoom;
		}
	}
}
