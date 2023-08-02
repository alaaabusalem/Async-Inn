﻿namespace Async_Inn.Models.Interfaces
{
	public interface IHotel
	{
		// CREATE
		Task<Hotel> Create(Hotel hotel);

		// GET ALL
		Task<List<Hotel>> GetHotels();

		// GET ONE BY ID
		Task<Hotel> GetHotel(int id);

		// UPDATE
		Task<Hotel> UpdateHotel(int id, Hotel hotel);

		// DELETE
		Task Delete(int id);
	}
}
