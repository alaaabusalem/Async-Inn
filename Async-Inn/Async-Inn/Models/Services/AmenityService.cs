using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class AmenityService : IAmenity
	{
		private readonly AsyncInnDbContext _context;

		public AmenityService(AsyncInnDbContext context) {
			_context = context;
		}
		
		public async Task<Amenity> Create(Amenity amenity)
		{
			await _context.Amenities.AddAsync(amenity);

			await _context.SaveChangesAsync();
			return amenity;
		}

		public async Task Delete(int id)
		{
			Amenity amenity = await _context.Amenities.FindAsync(id);
			if(amenity != null)
			{
				 _context.Amenities.Remove(amenity);
				_context.SaveChanges();	
			}
		}

		public async Task<List<Amenity>> GetAmenities()
		{
			var amenites = await _context.Amenities.ToListAsync();
			return amenites;
		}

		public async Task<Amenity> GetAmenity(int id)
		{
			var amenty = await _context.Amenities.FindAsync(id);
			return amenty;
		}

		public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
		{
			var amenty = await _context.Amenities.FindAsync(id);
			if(amenty != null) {
				_context.Amenities.Update(amenity);
				await _context.SaveChangesAsync();	
			}
			return amenity;
		}
	}
}
