using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace ShopMate.DTOs
{
    [Table("bills")]
    public class BillDTO : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("customerid")]
        public int? CustomerId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [Column("tax")]
        public decimal Tax { get; set; } = 0;

        [Column("discount")]
        public decimal Discount { get; set; } = 0;

        [Column("total")]
        public decimal Total { get; set; }

        [Column("paymenttype")]
        public string PaymentType { get; set; } = string.Empty;

        [Column("note")]
        public string Note { get; set; } = string.Empty;
    }
}