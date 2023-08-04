using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class AmenityService : IAmenity
	{
		private readonly AsyncInnDbContext _context;

		public AmenityService(AsyncInnDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// Create a new Amenity
		/// </summary>
		/// <param name="amenityDTO"></param>
		/// <returns></returns>
		public async Task<AmenityDTO> Create(AmenityDTO amenityDTO)
		{
			var amenity = new Amenity()
			{
				Name = amenityDTO.Name,
			};
			await _context.Amenities.AddAsync(amenity);

			await _context.SaveChangesAsync();

			return amenityDTO;
		}

		/// <summary>
		/// Delete an amenity
		/// </summary>
		/// <param name="id"> just provide the Id for Amenity you want to delete</param>
		/// <returns></returns>
		public async Task Delete(int id)
		{
			Amenity amenity = await _context.Amenities.FindAsync(id);
			if (amenity != null)
			{
				_context.Amenities.Remove(amenity);
				_context.SaveChanges();
			}
		}

		/// <summary>
		/// return all the Amenities in the data base
		/// </summary>
		/// <returns></returns>
		public async Task<List<AmenityDTO>> GetAmenities()
		{
			var amenites = await _context.Amenities.Select(A => new AmenityDTO
			{
				ID = A.Id,
				Name = A.Name,
			}).ToListAsync();
			return amenites;
		}

		/// <summary>
		/// return a spacific Amenity by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<AmenityDTO> GetAmenity(int id)
		{
			var amenty = await _context.Amenities.Select(A => new AmenityDTO
			{
				ID = A.Id,
				Name = A.Name,
			}).FirstAsync(Am => Am.ID == id);
			return amenty;
		}

		/// <summary>
		/// Update an existing Amenity
		/// </summary>
		/// <param name="id"></param>
		/// <param name="amenityDTO"></param>
		/// <returns></returns>
		public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenityDTO)
		{
			var amenity = await _context.Amenities.FirstAsync(Am => Am.Id == id);
			if (amenity == null) { return null; }
			amenity.Name = amenityDTO.Name;
			_context.Entry(amenity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return amenityDTO;
		}
	}
}
