using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
	public interface IRoom
	{

		/// <summary>
		/// add a new room
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		// CREATE
		Task<RoomDTO> Create(RoomDTO room);


		/// <summary>
		/// return all rooms
		/// </summary>
		/// <returns></returns>
		// GET ALL
		Task<List<RoomDTO>> GetRooms();

		/// <summary>
		/// return room by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET ONE BY ID
		Task<RoomDTO> GetRoom(int id);

		/// <summary>
		/// update room by Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="room"></param>
		/// <returns></returns>
		// UPDATE
		Task<RoomDTO> UpdateRoom(int id, RoomDTO room);


		/// <summary>
		/// delete room by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE
		Task Delete(int id);

		/// <summary>
		/// Add amenity to a room
		/// </summary>
		/// <param name="roomId"></param>
		/// <param name="amenityId"></param>
		/// <returns></returns>
		Task<RoomDTO> AddAmenityToRoom(int roomId, int amenityId);

		/// <summary>
		/// remove amentity from room
		/// </summary>
		/// <param name="roomId"></param>
		/// <param name="amenityId"></param>
		/// <returns></returns>
		Task RemoveAmentityFromRoom(int roomId, int amenityId);
	}
}
