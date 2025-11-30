using ShopMate.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    public class ProductManagementDL
    {
        public ProductManagementDL()
        {
        }
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            try
            {
                var client = SupabaseInitializer.Client;

                var response = await client
                    .From<ProductDTO>()
                    .Get();

                return response.Models;
            }
            catch (System.Exception)
            {
                return new List<ProductDTO>();
            }
        }

        public async Task<bool> AddProduct(ProductDTO pDTO)
        {
            try
            {
                var client = SupabaseInitializer.Client;

                var response = await client
                    .From<ProductDTO>()
                    .Insert(pDTO);

                return response.Models.Count > 0;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveProduct(int ID)
        {
            try
            {
                var client = SupabaseInitializer.Client;

                await client
                    .From<ProductDTO>()
                    .Where(p => p.ID == ID)
                    .Delete();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(ProductDTO pDTO)
        {
            try
            {
                var client = SupabaseInitializer.Client;

                await client
                    .From<ProductDTO>()
                    .Where(p => p.ID == pDTO.ID)
                    .Update(pDTO);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
