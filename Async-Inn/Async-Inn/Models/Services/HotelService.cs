﻿using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
	public class HotelService : IHotel
	{
		private readonly AsyncInnDbContext _context;
		public HotelService(AsyncInnDbContext context) {
		_context = context;	
		}	
		public async Task<Hotel> Create(Hotel hotel)
		{
			 await _context.AddAsync(hotel);
			await _context.SaveChangesAsync();
			return hotel;
		}

		public async Task Delete(int id)
		{
			var hotel= await _context.Hotels.FindAsync(id);
			if(hotel != null) {
			  _context.Hotels.Remove(hotel);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<Hotel>> GetHotels()
		{
			var hotels= await _context.Hotels.ToListAsync();
			return hotels;
		}

		public async Task<Hotel> GetHotel(int id)
		{
			var hotel = await _context.Hotels.FindAsync(id);

			return hotel;
			
		}

		public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
		{
			var hote = await _context.Hotels.FindAsync(id);
			if(hotel != null) {
				_context.Hotels.Update(hotel);
			  await _context.SaveChangesAsync();
				return hotel;
			}
			return null;
		}
	}
}
