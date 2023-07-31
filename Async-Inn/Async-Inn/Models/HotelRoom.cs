namespace Async_Inn.Models
{
	public class HotelRoom
	{
		public int RoomNumber { get; set; }

		public int HotelId { get; set; }
		public int RoomId { get; set; }

		public decimal Rate { get; set; }

		public bool PetFreindly { get; set; }

		public Hotel? hotel { get; set; }

		public Room? room { get; set; }
	}
}
