using System.Collections.Generic;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Services;

namespace Test_Asynch
{
	public class UnitTest1:MockDB
	{

		// Hotel Service Test
		[Fact]
		public async void TestGetAllHotelsInHotelService()
		{
			var hotelService = new HotelService(_db);
			var hotelList = new List<HotelDTO>()
			{

					new HotelDTO() { ID = 1, Name = "Async Inn Amman", City = "Amman", StreetAddress = " 12-12-12", State = "Amman", Phone = "12345" },
			new HotelDTO() { ID = 2, Name = "Async Inn Aqaba", City = "Aqaba", StreetAddress = " 13-13-13", State = "Aqaba", Phone = "67890" },
			new HotelDTO() { ID = 3, Name = "Async Inn Irbid", City = "Irbid", StreetAddress = " 14-14-14", State = "Irbid", Phone = "56437" }
			};
			// Act
			var AllHotels = await hotelService.GetHotels();
			Assert.Equal(hotelList.Count, AllHotels.Count);
		}

		[Fact]
		public async void TestCreatNewHotelInHotelService()
		{
			var hotelService = new HotelService(_db);
			var newHotel = new Hotel
			{
				Name = "TestHotel1",
				City = "TestHotel1",
				Country= "TestHotel1",
				Phone = "TestHotel1",
				State = "TestHotel1",
				StreetAddress = "TestHotel1"
			};
			
			// Act
			var hotelAdded = await hotelService.Create(newHotel);
			var AllHotels = await hotelService.GetHotels();
			Assert.Equal(4, AllHotels.Count);
		}

		[Fact]
		public async void TestGetAHotelById()
		{
			var hotelService = new HotelService(_db);
			var newHotel = new Hotel
			{
				Name = "TestHotel1",
				City = "TestHotel1",
				Country = "TestHotel1",
				Phone = "TestHotel1",
				State = "TestHotel1",
				StreetAddress = "TestHotel1"
			};

			// Act
			var hotelAdded = await hotelService.Create(newHotel);
			var hotel = await hotelService.GetHotel(4);
			Assert.Equal("TestHotel1", hotel.Name);
		}

		[Fact]
		public async void TestUpdateAHotel()
		{
			var hotelService = new HotelService(_db);
			var newHotel = new Hotel
			{
				Name = "TestHotel1",
				City = "TestHotel1",
				Country = "TestHotel1",
				Phone = "TestHotel1",
				State = "TestHotel1",
				StreetAddress = "TestHotel1"
			};
			var hotelDTO = new HotelDTO
			{
				ID=4,
				Name = "TestHotel1Change",
				City = "TestHotel1",
				Phone = "TestHotel1",
				State = "TestHotel1",
				StreetAddress = "TestHotel1"
			};
			// Act
			var hotelAdded = await hotelService.Create(newHotel);
			var hotel = await hotelService.UpdateHotel(4, hotelDTO);
			Assert.Equal("TestHotel1Change", hotel.Name);
		}

		[Fact]
		public async void TestDeleteAHotel()
		{
			var hotelService = new HotelService(_db);
			var newHotel = new Hotel
			{
				Name = "TestHotel1",
				City = "TestHotel1",
				Country = "TestHotel1",
				Phone = "TestHotel1",
				State = "TestHotel1",
				StreetAddress = "TestHotel1"
			};
			await hotelService.Create(newHotel);
			// Act
			 await hotelService.Delete(4);
			var hotels = await hotelService.GetHotels();
			Assert.Equal(3, hotels.Count());
		}

		//HotelRoom Service test

		//Creat a HotelRooms and get them all
		[Fact]
		public async void TestCreatAndGetAllHotelRooms()
		{
			var hotelRoomService = new HotelRoomService(_db);
			var hotelRoomDTO1 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 2,
				RoomNumber = 5,
				PetFriendly = true,
				Rate=3
			};
			var hotelRoomDTO2 = new HotelRoomDTO
			{
				HotelID = 2,
				RoomID = 3,
				RoomNumber = 33,
				PetFriendly = false,
				Rate = 10
			};
			var hotelRoomDTO3 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 3,
				RoomNumber =12,
				PetFriendly = true,
				Rate = 10
			};
			// Act
			await hotelRoomService.Create(hotelRoomDTO1,1);
			await hotelRoomService.Create(hotelRoomDTO2, 2);
			await hotelRoomService.Create(hotelRoomDTO3, 1);
			var AllHotelRooms = await hotelRoomService.GetHotelRooms(1);
			Assert.Equal(2, AllHotelRooms.Count);
		}

		[Fact]
		public async void TestGetHotelRoomById()
		{
			var hotelRoomService = new HotelRoomService(_db);
			var hotelRoomDTO1 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 2,
				RoomNumber = 5,
				PetFriendly = true,
				Rate = 3
			};
			var hotelRoomDTO2 = new HotelRoomDTO
			{
				HotelID = 2,
				RoomID = 3,
				RoomNumber = 33,
				PetFriendly = false,
				Rate = 10
			};
			var hotelRoomDTO3 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 3,
				RoomNumber = 12,
				PetFriendly = true,
				Rate = 10
			};
		
			await hotelRoomService.Create(hotelRoomDTO1, 1);
			await hotelRoomService.Create(hotelRoomDTO2, 2);
			await hotelRoomService.Create(hotelRoomDTO3, 1);
			// Act
			var hotelRoomDTO = await hotelRoomService.GetHotelRoom(1, 12);
			bool answer = hotelRoomDTO.PetFriendly;
			Assert.True(answer);
		}
		[Fact]
		public async void TestUpdateHotelRoom()
		{
			var hotelRoomService = new HotelRoomService(_db);
			var hotelRoomDTO1 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 2,
				RoomNumber = 5,
				PetFriendly = true,
				Rate = 3
			};
			var hotelRoomDTO2 = new HotelRoomDTO
			{
				HotelID = 2,
				RoomID = 3,
				RoomNumber = 33,
				PetFriendly = false,
				Rate = 10
			};
			var hotelRoomDTO3 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 3,
				RoomNumber = 12,
				PetFriendly = true,
				Rate = 10
			};
			var hotelRoomDTOToUpdate = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 3,
				RoomNumber = 12,
				PetFriendly = false,
				Rate = 10
			};
			await hotelRoomService.Create(hotelRoomDTO1, 1);
			await hotelRoomService.Create(hotelRoomDTO2, 2);
			await hotelRoomService.Create(hotelRoomDTO3, 1);
			// Act
			var hotelRoomDTO = await hotelRoomService.UpdateHotelRoom(1, 12, hotelRoomDTOToUpdate);
			bool answer = hotelRoomDTO.PetFriendly;
			Assert.False(answer);
		}

		[Fact]
		public async void TestDeleteHotelRoom()
		{
			var hotelRoomService = new HotelRoomService(_db);
			var hotelRoomDTO1 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 2,
				RoomNumber = 5,
				PetFriendly = true,
				Rate = 3
			};
			var hotelRoomDTO2 = new HotelRoomDTO
			{
				HotelID = 2,
				RoomID = 3,
				RoomNumber = 33,
				PetFriendly = false,
				Rate = 10
			};
			var hotelRoomDTO3 = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 3,
				RoomNumber = 12,
				PetFriendly = true,
				Rate = 10
			};
			var hotelRoomDTOToUpdate = new HotelRoomDTO
			{
				HotelID = 1,
				RoomID = 3,
				RoomNumber = 12,
				PetFriendly = false,
				Rate = 10
			};
			await hotelRoomService.Create(hotelRoomDTO1, 1);
			await hotelRoomService.Create(hotelRoomDTO2, 2);
			await hotelRoomService.Create(hotelRoomDTO3, 1);
			// Act
			await hotelRoomService.Delete(1, 12);
			var hotelThatDeleted = await hotelRoomService.GetHotelRoom(1, 12);
			Assert.Equal(null, hotelThatDeleted);
		}
	}
}