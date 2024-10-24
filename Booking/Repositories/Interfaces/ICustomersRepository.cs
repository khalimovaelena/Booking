using Booking.Models;

namespace Booking.Repositories.Interfaces
{
	public interface ICustomersRepository
	{
        Task<IList<Customer>> GetCustomers();
        Task<Customer> GetCustomer(Guid customerId);
	}
}

