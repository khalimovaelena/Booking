using System;
using Booking.Models;
using Booking.Services.Interfaces;

namespace Booking.Services
{
	public class GoogleBookingService: IBookingService
	{
        public Guid CustomerID { get; }

        public GoogleBookingService(Guid customerId)
		{
            CustomerID = customerId;
        }

        public string ServiceName => "GOOGLE Booking Service";

        public Task CreateBooking(RoomBooking booking)
        {
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
            //TODO: return bookings from Google external service for CustomerID
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

