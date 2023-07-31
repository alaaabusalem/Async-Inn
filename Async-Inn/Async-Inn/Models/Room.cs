namespace Async_Inn.Models
{
	public class Room
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }

		public List<RoomAmenity> roomAmenities { get; set; }

		public List<HotelRoom> hotelrooms { get; set; }
	}
}
