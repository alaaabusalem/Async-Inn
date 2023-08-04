using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
	public interface IAmenity
	{
		// CREATE
		/// <summary>
		/// add a new amenity
		/// </summary>
		/// <param name="amenity"></param>
		/// <returns></returns>
		Task<AmenityDTO> Create(AmenityDTO amenity);


		/// <summary>
		/// return all amenities
		/// </summary>
		/// <returns></returns>
		// GET ALL
		Task<List<AmenityDTO>> GetAmenities();

		/// <summary>
		/// return an amenity by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET ONE BY ID
		Task<AmenityDTO> GetAmenity(int id);

		/// <summary>
		/// Update an exist amenity
		/// </summary>
		/// <param name="id"></param>
		/// <param name="amenity"></param>
		/// <returns></returns>
		// UPDATE
		Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);


		/// <summary>
		/// delete an exist amenity by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE
		Task Delete(int id);
	}
}
