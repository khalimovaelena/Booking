using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Services;

namespace Booking.Repositories
{
    //TODO: implement connection to Database/CloudStorage etc
	public class CustomerRepository: ICustomersRepository
	{
        private IList<Customer> _customers = new List<Customer>();

		public CustomerRepository()
		{
            var customer1 = new Customer("Customer1", new MSBookingService(Guid.NewGuid()));
            var customer2 = new Customer("Customer2", new GoogleBookingService(Guid.NewGuid()));
            _customers.Add(customer1);
            _customers.Add(customer2);
        }

        public async Task<IList<Customer>> GetCustomers() => _customers; //TODO: get all customers from DB/CloudStorage etc

        public async Task<Customer> GetCustomer(Guid customerId)
        {
            return _customers?.SingleOrDefault<Customer>(c => c.ID.Equals(customerId));
        }
    }
}

