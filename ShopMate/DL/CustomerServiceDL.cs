using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    class CustomerServiceDL
    {
        public CustomerServiceDL()
        {
        }

        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<CustomerDTO>()
                    .Get();

                return response.Models;
            }
            catch (System.Exception)
            {
                return new List<CustomerDTO>();
            }
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

        public async Task<bool> RemoveCustomer(int ID)
        {
            try
            {
                var client = SupabaseInitializer.client;

                await client
                    .From<CustomerDTO>()
                    .Where(c => c.ID == ID)
                    .Delete();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomer(CustomerDTO cDTO)
        {
            try
            {
                var client = SupabaseInitializer.client;

                await client
                    .From<CustomerDTO>()
                    .Where(c => c.ID == cDTO.ID)
                    .Update(cDTO);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}