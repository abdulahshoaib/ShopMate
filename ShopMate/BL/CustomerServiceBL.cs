using ShopMate.DL;
using ShopMate.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    public class CustomerServiceBL
    {
        private readonly CustomerServiceDL csDL;

        public CustomerServiceBL()
        {
            csDL = new CustomerServiceDL();
        }

        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            return await csDL.GetAllCustomers();
        }

        public async Task<bool> AddCustomerAsync(CustomerDTO customerDTO)
        {
            return await csDL.AddCustomerAsync(customerDTO);
        }

        public async Task<bool> RemoveCustomer(int ID)
        {
            return await csDL.RemoveCustomer(ID);
        }

        public async Task<bool> UpdateCustomer(CustomerDTO customerDTO)
        {
            return await csDL.UpdateCustomer(customerDTO);
        }
    }
}
