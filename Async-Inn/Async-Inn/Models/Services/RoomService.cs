using System.Reflection;
using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Async_Inn.Models.Services
{
	public class RoomService : IRoom
	{
		private readonly AsyncInnDbContext _context;
		public RoomService(AsyncInnDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// add Amenity to an existing room
		/// </summary>
		/// <param name="roomId"></param>
		/// <param name="amenityId"></param>
		/// <returns></returns>
		public async Task<RoomDTO> AddAmenityToRoom(int roomId, int amenityId)
		{
			var room = await GetRoom(roomId);
			var amenity = await _context.Amenities.FindAsync(amenityId);
			if (room == null || amenity == null) { return null; }

			await _context.RoomAmenities.AddAsync(new RoomAmenity { RoomId = roomId, AmenityId = amenityId });
			await _context.SaveChangesAsync();

			var roomDTO = new RoomDTO
			{
				ID = roomId,
				Name = room.Name,
				Layout = room.Layout
			};
			return roomDTO;
		}

		/// <summary>
		/// add a new room
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		public async Task<RoomDTO> Create(RoomDTO room)
		{
			var roomToadd = new Room
			{
				Name = room.Name,
				Layout = room.Layout
			};
			await _context.AddAsync(roomToadd);
			await _context.SaveChangesAsync();
			return room;
		}


		/// <summary>
		/// delete an existing room
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task Delete(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room != null)
			{
				_context.Rooms.Remove(room);
				await _context.SaveChangesAsync();
			}
		}

		/// <summary>
		/// return a room by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<RoomDTO> GetRoom(int id)
		{
			var room = await _context.Rooms.Select(R => new RoomDTO
			{
				ID = R.Id,
				Name = R.Name,
				Layout = R.Layout,
				Amenities = R.roomAmenities.Select(a => new AmenityDTO
				{
					ID = a.amenity.Id,
					Name = a.amenity.Name
				}).ToList()
			})
				.FirstOrDefaultAsync(r => r.ID == id);

			return room;
		}


		/// <summary>
		/// return all rooms
		/// </summary>
		/// <returns></returns>
		public async Task<List<RoomDTO>> GetRooms()
		{
			var rooms = await _context.Rooms.Select(R => new RoomDTO
			{
				ID = R.Id,
				Name = R.Name,
				Layout = R.Layout,
				Amenities = R.roomAmenities.Select(a => new AmenityDTO
				{
					ID = a.amenity.Id,
					Name = a.amenity.Name
				}).ToList()

			}).ToListAsync();
			return rooms;
		}

		/// <summary>
		/// remove an amentity exsisting in a room
		/// </summary>
		/// <param name="roomId"></param>
		/// <param name="amenityId"></param>
		/// <returns></returns>
		public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
		{
			var RoomAminity = await _context.RoomAmenities.FindAsync(roomId, amenityId);
			if (RoomAminity != null)
			{
				_context.RoomAmenities.Remove(RoomAminity);
				await _context.SaveChangesAsync();
			}
		}


		/// <summary>
		/// update an existing room
		/// </summary>
		/// <param name="id"></param>
		/// <param name="roomTDO"></param>
		/// <returns></returns>
		public async Task<RoomDTO> UpdateRoom(int id, RoomDTO roomTDO)
		{
			var room = await _context.Rooms.FirstAsync(R => R.Id == id);
			if (room == null) { return null; }

			room.Name = roomTDO.Name;
			room.Layout = roomTDO.Layout;
			_context.Entry(room).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return roomTDO;
		}
	}
}
