using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services
{
    //TODO: Add UnitTests
	public class CustomersService: ICustomersService
	{
        private readonly ICustomersRepository _customerRepository;

		public CustomersService(ICustomersRepository customersRepository)
		{
            _customerRepository = customersRepository;
		}

        public async Task<IList<Customer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<Customer?> GetCustomer(Guid customerID)
        {
            //using repository to read Customers info from DB/CloudStorage/etc
            return await _customerRepository.GetCustomer(customerID);
        }
    }
}

