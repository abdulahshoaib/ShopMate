using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    internal class EmployManagementDL
    {
        public EmployManagementDL() { }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<EmployeeDTO>()
                    .Get();

                return response.Models;
            }
            catch (System.Exception)
            {
                return new List<EmployeeDTO>();
            }
        }

        public async Task<bool> AddEmployee(EmployeeDTO eDTO)
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<EmployeeDTO>()
                    .Insert(eDTO);

                return response.Models.Count > 0;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveEmployee(int ID)
        {
            try
            {
                var client = SupabaseInitializer.client;

                await client
                    .From<UserDTO>()
                    .Where(u => u.EmployeeID == ID)
                    .Delete();

                await client
                    .From<EmployeeDTO>()
                    .Where(e => e.ID == ID)
                    .Delete();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> UpdateEmployee(EmployeeDTO eDTO)
        {
            try
            {
                var client = SupabaseInitializer.client;

                await client
                    .From<EmployeeDTO>()
                    .Where(e => e.ID == eDTO.ID)
                    .Update(eDTO);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
