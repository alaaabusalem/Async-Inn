namespace Async_Inn.Models.Interfaces
{
	public interface IRoom
	{
		// CREATE
		Task<Room> Create(Room room);

		// GET ALL
		Task<List<Room>> GetRooms();

		// GET ONE BY ID
		Task<Room> GetRoom(int id);

		// UPDATE
		Task<Room> UpdateRoom(int id, Room room);

		// DELETE
		Task Delete(int id);

		// Add Amenity to your room
		Task<Room> AddAmenityToRoom(int roomId, int amenityId);

		// Remove Amenity from your room
		Task<Room> RemoveAmentityFromRoom(int roomId, int amenityId);
	}
}
