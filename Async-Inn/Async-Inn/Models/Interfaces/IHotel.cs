using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
	public interface IHotel
	{

		/// <summary>
		/// creat an hotel
		/// </summary>
		/// <param name="hotel"></param>
		/// <returns></returns>
		// CREATE
		Task<Hotel> Create(Hotel hotel);


		/// <summary>
		/// return all hotels
		/// </summary>
		/// <returns></returns>
		// GET ALL
		Task<List<HotelDTO>> GetHotels();


		/// <summary>
		/// return hotel by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET ONE BY ID
		Task<HotelDTO> GetHotel(int id);

		/// <summary>
		/// update hotel by Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="hotelDTO"></param>
		/// <returns></returns>
		// UPDATE
		Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO);


		/// <summary>
		/// delete hotel by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE
		Task Delete(int id);
	}
}
