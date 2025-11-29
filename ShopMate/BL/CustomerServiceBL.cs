using ShopMate.DTOs;
using ShopMate.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    public class CustomerServiceBL
    {
        private readonly CustomerServiceDL csDL;

        public CustomerServiceBL() {
            csDL = new CustomerServiceDL();
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
