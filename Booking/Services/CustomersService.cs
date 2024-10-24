using Booking.Models;
using Booking.Services.Interfaces;

namespace Booking.Services
{
	public class CustomersService: ICustomersService
	{
		public CustomersService()
		{
		}

        public async Task<Customer?> GetCustomer(Guid customerID)
        {
            //TODO: use repository to read Customers info from "DB
            return new Customer("Default Customer", new MSBookingService(customerID));
        }
    }
}

