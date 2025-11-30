using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    public class UserManagementDL
    {

        public UserManagementDL() { }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<UserDTO>()
                    .Get();

               return response.Models.ToList();
            }
            catch
            {
                return new List<UserDTO>();
            }
        }

        public async Task<UserDTO?> GetUser(int EmployeeID)
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<UserDTO>()
                    .Where(u => u.EmployeeID == EmployeeID)
                    .Get();

                return response.Models.FirstOrDefault();
            }   
            catch 
            {
                return null;
            }
        }

        public async Task<bool> AddUser(UserDTO user)
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<UserDTO>()
                    .Insert(user);

                return response.Models.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<UserDTO>()
                    .Where(c => c.ID == user.ID)
                    .Update(user);

                return response.Models.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
