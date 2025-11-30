using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    public class BillManagementDL
    {
        public async Task<int?> CreateBill(BillDTO billDTO, List<BillItemDTO> items)
        {
            try
            {
                var client = SupabaseInitializer.client;

                var billResponse = await client
                    .From<BillDTO>()
                    .Insert(billDTO);

                if (billResponse.Models.Count == 0)
                    return null;

                var createdBill = billResponse.Models[0];
                int billId = createdBill.Id;

                foreach (var item in items)
                {
                    item.BillId = billId;
                    item.LineTotal = item.UnitPrice * item.Quantity;
                }

                var itemsResponse = await client
                    .From<BillItemDTO>()
                    .Insert(items);

                if (itemsResponse.Models.Count == 0)
                    return null;

                return billId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<BillDTO>> GetAllBills()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<BillDTO>()
                    .Get();

                return response.Models;
            }
            catch (System.Exception)
            {
                return new List<BillDTO>();
            }
        }

        public async Task<List<BillItemDTO>> GetBillItems(int billId)
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<BillItemDTO>()
                    .Where(bi => bi.BillId == billId)
                    .Get();

                return response.Models;
            }
            catch (System.Exception)
            {
                return new List<BillItemDTO>();
            }
        }

        public async Task<decimal> GetTotalSalesToday()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var todayStart = DateTime.Today;
                var tomorrowStart = DateTime.Today.AddDays(1);

                var response = await client
                    .From<BillDTO>()
                    .Where(b => b.CreatedAt >= todayStart && b.CreatedAt < tomorrowStart)
                    .Get();
                
                return response.Models.Sum(b => b.Total);
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public async Task<int> GetTotalBillsToday()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var todayStart = DateTime.Today;
                var tomorrowStart = DateTime.Today.AddDays(1);

                var response = await client
                    .From<BillDTO>()
                    .Where(b => b.CreatedAt >= todayStart && b.CreatedAt < tomorrowStart)
                    .Get();

                return response.Models.Count;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public async Task<int> GetLowStockProductsCount()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var response = await client
                    .From<ProductDTO>()
                    .Where(p => p.Stock <= p.LowStockLimit)
                    .Select("ID")
                    .Get();

                return response.Models.Count;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public async Task<decimal> GetAverageBillValueToday()
        {
            try
            {
                var client = SupabaseInitializer.client;

                var todayStart = DateTime.Today;
                var tomorrowStart = DateTime.Today.AddDays(1);

                var response = await client
                    .From<BillDTO>()
                    .Where(b => b.CreatedAt >= todayStart && b.CreatedAt < tomorrowStart)
                    .Get();

                if (response.Models.Count == 0)
                    return 0;

                return response.Models.Average(b => b.Total);
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
    }
}
