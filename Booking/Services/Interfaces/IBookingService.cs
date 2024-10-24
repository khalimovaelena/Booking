using Booking.Models;

namespace Booking.Services.Interfaces
{
	public interface IBookingService
	{
        /// <summary>
        /// Name of the Booking service that stores all Customer's bookings
        /// </summary>
        string ServiceName { get; }

       Task<IList<RoomBooking>> GetBookings(DateTime rangeStart, DateTime rangeEnd);

       Task CreateBooking(RoomBooking booking);
    }
}

