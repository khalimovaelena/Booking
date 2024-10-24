using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<IList<Customer>> GetCustomers();
        Task<Customer?> GetCustomer(Guid customerID);
    }
}