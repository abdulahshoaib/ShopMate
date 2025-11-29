using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DTOs
{
    public class BillDTO
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int UserId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}

