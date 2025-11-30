using ShopMate.DL;
using ShopMate.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    public class UserManagementBL
    {
        private readonly UserManagementDL umDL = new UserManagementDL();


        public async Task<List<UserDTO>> GetAllUsers()
        {
            return await umDL.GetAllUsers();
        }
        public async Task<UserDTO?> GetUser(int EmpolyeeID)
        {
            return await umDL.GetUser(EmpolyeeID);
        }

        public async Task<bool> AddUser(UserDTO user)
        {
            return await umDL.AddUser(user);
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {
            return await umDL.UpdateUser(user);
        }
    }
}
