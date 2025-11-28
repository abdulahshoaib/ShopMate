using ShopMate.DL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool AddProduct(ProductDTO pDTO)
        {
            return pmDL.AddProduct(pDTO);
        }

        public bool RemoveProduct(ProductDTO pDTO)
        {
            return pmDL.RemoveProduct(pDTO.ID);
        }

        public bool UpdateProduct(ProductDTO pDTO)
        {
            return pmDL.UpdateProduct(pDTO);
        }
    }
}
