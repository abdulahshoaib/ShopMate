using ShopMate.DL;
using ShopMate.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    public class EmployManagementBL
    {
        private readonly EmployManagementDL emDL;
        public EmployManagementBL()
        {
            emDL = new EmployManagementDL();
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            return await emDL.GetAllEmployees();
        }

        public async Task<EmployeeDTO> GetEmployee(int EmployeeID)
        {
            return await emDL.GetEmployee(EmployeeID);
        }

        public async Task<int?> AddEmployee(EmployeeDTO eDTO)
        {
            return await emDL.AddEmployee(eDTO);
        }

        public async Task<bool> RemoveEmployee(EmployeeDTO eDTO)
        {
            return await emDL.RemoveEmployee(eDTO.ID);
        }

        public async Task<bool> UpdateEmployee(EmployeeDTO eDTO)
        {
            return await emDL.UpdateEmployee(eDTO);
        }
    }
}
