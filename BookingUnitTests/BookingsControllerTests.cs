using System.Globalization;
using Booking.Models;
using Booking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Booking.Controllers.Tests;

public class BookingControllerTests
{
    private BookingsController controller;
    private Mock<ICustomersService> customersService;

    [SetUp]
    public void Setup()
    {
        var logger = new Mock<ILogger<BookingsController>>();
        customersService = new Mock<ICustomersService>();
        controller = new BookingsController(logger.Object, customersService.Object);
    }

    [Test]
    public async Task GetBookings_ReturnsOk()
    {
        //Arrange
        var customerId = Guid.NewGuid();
        var startTime = DateTime.Now;
        var endTime = startTime.AddMinutes(30);

        var bookings = new List<RoomBooking>();
        bookings.Add(new RoomBooking
        {
            ID = Guid.NewGuid(),
            StartTime = startTime,
            EndTime = endTime,
            Title = $"Test Booking for Customer = {customerId}",
            CustomerID = customerId,
            RoomID = Guid.NewGuid(),
            UserID = Guid.NewGuid(),
        });

        var customerBookingService = new Mock<IBookingService>();

        var customer = new Customer("Test Customer", customerBookingService.Object, customerId);

        customersService.Setup(c => c.GetCustomer(It.IsAny<Guid>())).ReturnsAsync(customer);
        customerBookingService.Setup(s => s.GetBookings(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(bookings);

        //Act
        var result = await controller.GetBookingsForCustomer(customerId, startTime, endTime);
        var okResult = result.Result as OkObjectResult;

        //Assert
        Assert.IsNotNull(okResult);
        Assert.IsNotNull(okResult.Value);
        var resultBookings = (IEnumerable<RoomBooking>)okResult.Value;
        Assert.NotNull(resultBookings);
        Assert.IsNotEmpty(resultBookings);
        Assert.That(resultBookings, Is.EqualTo(bookings));
    }

    [Test]
    public async Task GetBookings_CustomerIdIsEmpty_ReturnsBadRequest()
    {
        //Arrange
        var customerId = Guid.Empty;
        var startTime = DateTime.Now;
        var endTime = startTime.AddMinutes(30);

        //Act
        var result = await controller.GetBookingsForCustomer(customerId, startTime, endTime);
        var badRequestResult = result.Result as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.Value, Is.EqualTo("CustomerID must be not empty"));
    }

    [Test]
    public async Task GetBookings_CustomerIdNotFound_ReturnsBadRequest()
    {
        //Arrange
        var customerId = Guid.NewGuid();
        var startTime = DateTime.Now;
        var endTime = startTime.AddMinutes(30);
        Customer? customer = null;
        customersService.Setup(c => c.GetCustomer(It.IsAny<Guid>())).ReturnsAsync(customer);

        //Act
        var result = await controller.GetBookingsForCustomer(customerId, startTime, endTime);
        var notFoundResult = result.Result as NotFoundObjectResult;

        //Assert
        Assert.IsNotNull(notFoundResult);
        Assert.That(notFoundResult.Value, Is.EqualTo($"Customer with id = {customerId} is not found"));
    }

    [TestCase("25.10.2024T12:00", "25.10.2024T12:00")]
    [TestCase("25.10.2024T12:00", "25.10.2024T11:00")]
    [TestCase("25.10.2024T12:00", "24.10.2024T12:00")]
    [Test]
    public async Task GetBookings_WrongStartEndDate_ReturnsBadRequest(string startDate, string endDate)
    {
        //Arrange
        var customerId = Guid.NewGuid();
        var startTime = DateTime.ParseExact(startDate, "dd.MM.yyyyTHH:mm", CultureInfo.InvariantCulture);
        var endTime = DateTime.ParseExact(endDate, "dd.MM.yyyyTHH:mm", CultureInfo.InvariantCulture);

        var customerBookingService = new Mock<IBookingService>();

        var customer = new Customer("Test Customer", customerBookingService.Object, customerId);

        customersService.Setup(c => c.GetCustomer(It.IsAny<Guid>())).ReturnsAsync(customer);


        //Act
        var result = await controller.GetBookingsForCustomer(customerId, startTime, endTime);
        var badRequestResult = result.Result as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.Value, Is.EqualTo("Start Time must be less than End Time"));
    }

    //TODO: add test when GetBookings by service throws exception
}
