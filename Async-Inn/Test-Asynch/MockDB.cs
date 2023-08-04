using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Async_Inn.Data;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Models.DTOs;
using Async_Inn.Models;

namespace Test_Asynch
{
	public abstract class MockDB : IDisposable
	{


		private readonly SqliteConnection _connection;

		protected readonly AsyncInnDbContext _db;
        public MockDB()
        {
            _connection = new SqliteConnection();
			_connection.Open();
			_db = new AsyncInnDbContext(
				new DbContextOptionsBuilder<AsyncInnDbContext> ()
				.UseSqlite(_connection).Options);
			_db.Database.EnsureCreated();
        }
        public void Dispose()
		{
			_db?.Dispose();	
			_connection?.Dispose();	
		}


		protected async Task<HotelDTO> CreateAndSaveTestHotel()
		{
			var hotel = new Hotel() { 
				Name="TestHotel", 
				City="TestHotel", 
				Country="TestHotel",
				Phone= "TestHotel",
				State= "TestHotel",
				StreetAddress= "TestHotel"
			};
			_db.Hotels.Add(hotel);

			await _db.SaveChangesAsync();

			var hotelDTO= new HotelDTO()
			{
				Name = "TestHotel",
				City = "TestHotel",
				Phone = "TestHotel",
				State = "TestHotel",
				StreetAddress = "TestHotel"
			};
			return hotelDTO;
		}

		protected async Task<RoomDTO> CreateAndSaveTestRoom()
		{
			var room = new Room()
			{
				Name = "TestRoom",
				Layout=0
				
			};
			_db.Rooms.Add(room);

			await _db.SaveChangesAsync();

			var roomDTO = new RoomDTO()
			{
				Name = "TestRoom",
				Layout = 0
			};
			return roomDTO;
		}
	}
}
