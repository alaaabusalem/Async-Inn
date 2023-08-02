using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class HotelRoomService : IHotelRoom
	{
		private readonly AsyncInnDbContext _context;
		public HotelRoomService(AsyncInnDbContext context)
		{

			_context = context;
		}
		public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int HotelId)
		{
			var Hotel = await _context.Hotels.FindAsync(HotelId);
			//var Room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
			if (Hotel != null)
			{
				HotelRoom HR = new HotelRoom()
				{
					HotelId = HotelId,
					RoomId = hotelRoom.RoomID,
					RoomNumber = hotelRoom.RoomNumber,
					Rate = hotelRoom.Rate,
					PetFreindly = hotelRoom.PetFriendly

				};
				await _context.HotelRooms.AddAsync(HR);
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
				.FirstOrDefaultAsync(HR => HR.HotelId == hotelId && HR.RoomNumber == roomNumber);
			//var hotelroom = await _context.HotelRooms.GetById(hotelId, roomNumber);
			if (hotelroom != null)
			{
				_context.HotelRooms.Remove(hotelroom);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
		{
			var Room = await _context.HotelRooms.Select(HR => new HotelRoomDTO
			{
				HotelID = HR.HotelId,
				RoomNumber = HR.RoomNumber,
				Rate = HR.Rate,
				PetFriendly = HR.PetFreindly,
				RoomID = HR.RoomId,
				Room = new RoomDTO
				{
					ID = HR.room.Id,
					Name = HR.room.Name,
					Layout = HR.room.Layout,
					Amenities = HR.room.roomAmenities.Select(RA => new AmenityDTO
					{
						ID = RA.amenity.Id,
						Name = RA.amenity.Name
					}).ToList()
				}
			})

		.FirstOrDefaultAsync(HR => HR.HotelID == hotelId && HR.RoomNumber == roomNumber);
			return Room;
		}

		public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
		{
			var Rooms = await _context.HotelRooms.Select(HR => new HotelRoomDTO
			{
				HotelID = HR.HotelId,
				RoomNumber = HR.RoomNumber,
				Rate = HR.Rate,
				PetFriendly = HR.PetFreindly,
				RoomID = HR.RoomId,
				Room = new RoomDTO
				{
					ID = HR.room.Id,
					Name = HR.room.Name,
					Layout = HR.room.Layout,
					Amenities = HR.room.roomAmenities.Select(RA => new AmenityDTO
					{
						ID = RA.amenity.Id,
						Name = RA.amenity.Name
					}).ToList()
				}
			}).Where(HR => HR.HotelID == hotelId).ToListAsync();
			return Rooms;
		}

		public async Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoom)
		{

			var HotelRoomToUpdate = await _context.HotelRooms.FirstAsync(HR => HR.HotelId == hotelId && HR.RoomNumber == roomNumber);

			if (HotelRoomToUpdate == null) { return null; }
			HotelRoomToUpdate.Rate = hotelRoom.Rate;
			HotelRoomToUpdate.PetFreindly = hotelRoom.PetFriendly;
			HotelRoomToUpdate.RoomId = hotelRoom.RoomID;
			_context.Entry(HotelRoomToUpdate).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return hotelRoom;
		}
	}
}
