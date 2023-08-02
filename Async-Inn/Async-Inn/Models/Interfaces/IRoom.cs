using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
	public interface IRoom
	{
		// CREATE
		Task<RoomDTO> Create(RoomDTO room);

		// GET ALL
		Task<List<RoomDTO>> GetRooms();

		// GET ONE BY ID
		Task<RoomDTO> GetRoom(int id);

		// UPDATE
		Task<RoomDTO> UpdateRoom(int id, RoomDTO room);

		// DELETE
		Task Delete(int id);
		Task<RoomDTO> AddAmenityToRoom(int roomId, int amenityId);
		Task RemoveAmentityFromRoom(int roomId, int amenityId);
	}
}
