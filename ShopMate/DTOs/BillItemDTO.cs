using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ShopMate.DTOs
{
    [Table("billitems")]
    public class BillItemDTO : BaseModel
    {
        [PrimaryKey("id", false)]
        [Column("id")]
        public int Id { get; set; }

        [Column("billid")]
        public int BillId { get; set; }

        [Column("productid")]
        public int? ProductId { get; set; }

        [Column("productname")]
        public string ProductName { get; set; } = string.Empty;

        [Column("unitprice")]
        public decimal UnitPrice { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("linetotal")]
        public decimal LineTotal { get; set; }
    }
}