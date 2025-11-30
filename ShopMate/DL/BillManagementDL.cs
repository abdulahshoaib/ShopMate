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

            // 1. Insert the bill
            var billResponse = await client
                .From<BillDTO>()
                .Insert(billDTO);

            if (billResponse.Models.Count == 0)
                return null;

            var createdBill = billResponse.Models[0];
            int billId = createdBill.Id;

            // Prepare items for insertion
            foreach (var item in items)
            {
                item.BillId = billId;
                item.LineTotal = item.UnitPrice * item.Quantity;
            }

            // 2. Insert all bill items
            var itemsResponse = await client
                .From<BillItemDTO>()
                .Insert(items);

            if (itemsResponse.Models.Count == 0)
            {
                // Optional: Implement rollback logic here if items insertion fails
                return null;
            }

            // --- 3. DECREMENT STOCK (New Logic) ---

            // Group items by ProductId to find the total quantity sold for each product
            var soldProducts = items
                .Where(i => i.ProductId.HasValue)
                .GroupBy(i => i.ProductId.Value)
                .Select(g => new
                {
                    ProductId = g.Key,
                    QuantitySold = g.Sum(i => i.Quantity)
                })
                .ToList();

            // Perform atomic update for each unique product
            foreach (var productSale in soldProducts)
            {
                // Create a temporary ProductDTO object to hold the ID
                var productToUpdate = new ProductDTO { Id = productSale.ProductId };

                // Set the update dictionary for the decrement operation
                var updatePayload = new Dictionary<string, object>
            {
                // Tells PostgREST to subtract the QuantitySold from the 'stock' column
                {"stock", productSale.QuantitySold}
            };

                // Use the client's Update method with the Set method for atomic decrement
                // This translates to: SET stock = stock - <QuantitySold> WHERE id = <ProductId>
                await client
                    .From<ProductDTO>()
                    .Where(p => p.ID == productSale.ProductId)
                    .Decrement(productToUpdate, updatePayload);
            }

            // --- END DECREMENT STOCK ---

            return billId;
        }
        catch (Exception)
        {
            // Log the exception (recommended)
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
                    .Select("stock,lowstocklimit")
                    .Get();

                var lowStockCount = response.Models
                    .Count(p => p.Stock <= p.LowStockLimit);

                return lowStockCount;
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
