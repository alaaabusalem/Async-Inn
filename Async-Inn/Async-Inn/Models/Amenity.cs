﻿namespace Async_Inn.Models
{
	public class Amenity
	{
        public int Id { get; set; }
        public string Name { get; set; }

		public List<RoomAmenity>? roomamenity { get; set; }
	}
}
