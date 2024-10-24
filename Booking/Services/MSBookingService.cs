using Booking.Models;
using Booking.Services.Interfaces;

namespace Booking.Services
{
	public class MSBookingService: IBookingService
	{
        public Guid CustomerID { get; }

		public MSBookingService(Guid customerId)
		{
            CustomerID = customerId;
		}

        public string ServiceName => "Microsoft Booking Service";

        public async Task CreateBooking(RoomBooking booking)
        {
            //TODO: book a room in MS
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all bookings for the Customer between <paramref name="rangeStart"/> and <paramref name="rangeEnd"/>
        /// </summary>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <returns>List of all RoomBookings within 2 dates</returns>
        public async Task<IList<RoomBooking>> GetBookings(DateTime rangeStart, DateTime rangeEnd)
        {
            //TODO: return bookings from MS external service for CustomerID
            var bookings = new List<RoomBooking>();
            bookings.Add(new RoomBooking
            {
                ID = Guid.NewGuid(),
                StartTime = rangeStart,
                EndTime = rangeEnd,
                Title = $"MS Booking for Customer = {CustomerID}",
                CustomerID = CustomerID,
                RoomID = Guid.NewGuid(),
                UserID = Guid.NewGuid(),
            });

            return bookings;
        }
    }
}

