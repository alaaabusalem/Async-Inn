namespace Async_Inn.Models
{
	public class RoomAmenity
	{
		public int RoomId { get; set; }
		public int AmenityId { get; set; }

        public Room room { get; set; }

		public Amenity amenity { get; set; }
	}
}
