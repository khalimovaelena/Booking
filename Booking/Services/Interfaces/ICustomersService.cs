using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<Customer?> GetCustomer(Guid customerID);
    }
}