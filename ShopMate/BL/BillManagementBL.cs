using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopMate.DL;
using ShopMate.DTOs;

namespace ShopMate.BL
{
    public class BillManagementBL
    {
        private readonly BillManagementDL bmDL;
        public BillManagementBL()
        {
            bmDL = new BillManagementDL();
        }

        // Return the BillID from the DataBase
        public async Task<int?> CreateBill(BillDTO bDTO, List<BillItemDTO> items)
        {
            return await bmDL.CreateBill(bDTO, items);
        }

        public async Task<List<BillDTO>> GetAllBills()
        {
            return await bmDL.GetAllBills();
        }

        public async Task<List<BillItemDTO>> GetBillItems(int billID)
        {
            return await bmDL.GetBillItems(billID);
        }

        // === KPIs ===

        // KP1: Total Sales Amount
        public async Task<decimal> GetTotalSalesToday()
        {
            return await bmDL.GetTotalSalesToday();
        }

        // KPI2: Total Bills
        public async Task<int> GetTotalBillsToday()
        {
            return await bmDL.GetTotalBillsToday();
        }


        // KPI3: Average Bill Value Today
        public async Task<decimal> GetAverageBillValueToday()
        {
            return await bmDL.GetAverageBillValueToday();
        }

        // KPI4: Low Stock Products Count (products below low stock limit)
        public async Task<int> GetLowStockProductsCount()
        {
            return await bmDL.GetLowStockProductsCount();
        }

    }
}