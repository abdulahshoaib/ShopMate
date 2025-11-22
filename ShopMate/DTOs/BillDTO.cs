using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DTOs
{
    internal class BillDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<BillItemDTO> Items { get; set; } = new List<BillItemDTO>();
        public decimal Total => Items.Sum(i => i.Total);
    }
}
