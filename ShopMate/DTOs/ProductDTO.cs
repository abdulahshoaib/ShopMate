using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace ShopMate.DTOs
{
    [Table("products")]
    public class ProductDTO : BaseModel
    {
        public ProductDTO()
        {
            Name = "";
            Description = "";
        }

        [PrimaryKey("id", false)]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("expirydate")]
        public DateTime? ExpiryDate { get; set; }

        [Column("lowstocklimit")]
        public int LowStockLimit { get; set; } = 5;
    }
}