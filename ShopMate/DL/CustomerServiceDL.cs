using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    class CustomerServiceDL
    {
        public CustomerServiceDL() {
            
        }

        public async Task<bool> AddCustomerAsync(CustomerDTO cDTO)
        {
            var client = SupabaseInitializer.client;
            var newCustomer = new CustomerDTO
            {
                Name = cDTO.Name,
                Phone = cDTO.Phone,
                Address = cDTO.Address,
                Age = cDTO.Age,
                Gender = cDTO.Gender
            };
            var response = await client.From<CustomerDTO>().Insert(cDTO);
            return response.Models.Count > 0;
        }

        public bool RemoveCustomer(int ID)
        {
            // TODO: Add DB call to remove customer to the customers table
            return true;
        }

        public bool UpdateCustomer(CustomerDTO cDTO)
        {
            // TODO: Add DB to update the incoming customer's data
            return true; 
        }
    }
}