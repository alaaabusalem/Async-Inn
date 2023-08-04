using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
	public interface IHotelRoom
	{
		/// <summary>
		///  add a new hotel room
		/// </summary>
		/// <param name="hotelRoom"></param>
		/// <param name="HotelId"></param>
		/// <returns></returns>
		Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int HotelId);


		/// <summary>
		///  return all hotel rooms
		/// </summary>
		/// <param name="hotelId"></param>
		/// <returns></returns>
		// GET ALL
		Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);


		/// <summary>
		/// return an hotel room  by hotel Id and room number
		/// </summary>
		/// <param name="hotelId"></param>
		/// <param name="roomNumber"></param>
		/// <returns></returns>
		// GET ONE BY ID
		Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);
		/// <summary>
		/// update an hotel room
		/// </summary>
		/// <param name="hotelId"></param>
		/// <param name="roomNumber"></param>
		/// <param name="hotelRoom"></param>
		/// <returns></returns>
		// UPDATE
		Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoom);

		/// <summary>
		/// delete an hotel room
		/// </summary>
		/// <param name="hotelId"></param>
		/// <param name="roomNumber"></param>
		/// <returns></returns>
		// DELETE
		Task Delete(int hotelId, int roomNumber);
	}
}

