using Booking.Services.Interfaces;

namespace Booking.Models
{
	public class Customer
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
		public IBookingService BookingService { get; set; }

		public Customer(string name, IBookingService service, Guid? id = null)
		{
			ID = id ?? Guid.NewGuid();
			Name = name;
			BookingService = service;
		}
	}
}

