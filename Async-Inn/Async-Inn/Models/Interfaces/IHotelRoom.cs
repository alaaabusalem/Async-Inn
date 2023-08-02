namespace Async_Inn.Models.Interfaces
{
	public interface IHotelRoom
	{

		Task<HotelRoom> Create(HotelRoom hotelRoom,int HotelId);

		// GET ALL
		Task<List<HotelRoom>> GetHotelRooms(int hotelId);

		// GET ONE BY ID
		Task<HotelRoom> GetHotelRoom(int hotelId,int roomNumber);

		// UPDATE
		Task<HotelRoom> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom);

		// DELETE
		Task Delete(int hotelId, int roomNumber);
	}
}

