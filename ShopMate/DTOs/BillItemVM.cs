using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DTOs
{
    public class BillItemVM
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        private int qty;

        public int Quantity
        {
            get => qty;
            set
            {
                qty = value;
                Total = UnitPrice * qty;
            }
        }

        public decimal Total { get; private set; }
    }
}