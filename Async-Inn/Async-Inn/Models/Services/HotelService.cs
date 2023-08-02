using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

		public async Task<List<HotelDTO>> GetHotels()
		{
			var hotels = await _context.Hotels.Select(hotel => new HotelDTO
			{
				ID = hotel.Id,
				Name = hotel.Name,
				StreetAddress = hotel.StreetAddress,
				City = hotel.City,
				State = hotel.State,
				Phone = hotel.Phone,
				Rooms = hotel.hotelrooms.Select(HR => new HotelRoomDTO
				{
					HotelID = HR.HotelId,
					RoomNumber = HR.RoomNumber,
					Rate = HR.Rate,
					PetFriendly = HR.PetFreindly,
					RoomID = HR.RoomId,
					Room = /*HR.room.Select(Room =>*/ new RoomDTO
					{
						ID = HR.room.Id,
						Name = HR.room.Name,
						Layout = HR.room.Layout,
						Amenities = HR.room.roomAmenities.Select(a => new AmenityDTO
						{

							ID = a.amenity.Id,
							Name = a.amenity.Name,
						}).ToList()
					}
				}).ToList()
			})
				.ToListAsync();
			return hotels;
		}

		public async Task<HotelDTO> GetHotel(int id)
		{
			var hotel = await _context.Hotels.Select(hotel => new HotelDTO
			{
				ID = hotel.Id,
				Name = hotel.Name,
				StreetAddress = hotel.StreetAddress,
				City = hotel.City,
				State = hotel.State,
				Phone = hotel.Phone,
				Rooms = hotel.hotelrooms.Select(HR => new HotelRoomDTO
				{
					HotelID = HR.HotelId,
					RoomNumber = HR.RoomNumber,
					Rate = HR.Rate,
					PetFriendly = HR.PetFreindly,
					RoomID = HR.RoomId,
					Room = /*HR.room.Select(Room =>*/ new RoomDTO
					{
						ID = HR.room.Id,
						Name = HR.room.Name,
						Layout = HR.room.Layout,
						Amenities = HR.room.roomAmenities.Select(a => new AmenityDTO
						{

							ID = a.amenity.Id,
							Name = a.amenity.Name,
						}).ToList()
					}
				}).ToList()
			})
				.FirstOrDefaultAsync(H=> H.ID==id);

			return hotel;
			
		}

		public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO)
		{
			Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(H => H.Id == hotelDTO.ID);
			if(hotel == null)
			{
				return null;
			}
			hotel.Name = hotelDTO.Name;
			hotel.State = hotelDTO.State;
			hotel.StreetAddress = hotelDTO.StreetAddress;
			hotel.Phone = hotelDTO.Phone;
			hotel.City= hotelDTO.City;	
			_context.Entry(hotel).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return  hotelDTO;
		}
	}
}
