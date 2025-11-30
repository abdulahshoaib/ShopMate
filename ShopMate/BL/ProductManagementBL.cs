using ShopMate.DL;
using ShopMate.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    public class ProductManagementBL
    {
        private readonly ProductManagementDL pmDL;

        public ProductManagementBL()
        {
            pmDL = new ProductManagementDL();
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            return await pmDL.GetAllProducts();
        }

        public async Task<bool> AddProduct(ProductDTO pDTO)
        {
            return await pmDL.AddProduct(pDTO);
        }

        public async Task<bool> RemoveProduct(ProductDTO pDTO)
        {
            return await pmDL.RemoveProduct(pDTO.ID);
        }

        public async Task<bool> UpdateProduct(ProductDTO pDTO)
        {
            return await pmDL.UpdateProduct(pDTO);
        }
    }
}
