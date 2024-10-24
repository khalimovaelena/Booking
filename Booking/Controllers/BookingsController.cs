using Booking.Models;
using Booking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly ICustomersService _customersService;

    public BookingsController(
        ILogger<BookingsController> logger,
        ICustomersService customersService)
    {
        _logger = logger;
        _customersService = customersService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomBooking>>> GetBookingsForCustomer(Guid customerID, DateTime startTime, DateTime endTime)
    {
        try
        {
            if (customerID == Guid.Empty)
            {
                return BadRequest("CustomerID must be not empty");
            }

            var customer = await _customersService.GetCustomer(customerID);
            if (customer == null)
            {
                return NotFound($"Customer with id = {customerID} is not found");
            }

            if (startTime >= endTime)
            {
                return BadRequest("Start Time must be less than End Time");
            }

            IList<RoomBooking> bookings = await customer.BookingService.GetBookings(startTime, endTime);
            _logger.LogInformation($"Used service {customer.BookingService.ServiceName} to GET bookings for customer with ID = {customerID}");
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}

