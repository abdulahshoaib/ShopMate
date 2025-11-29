using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ShopMate.DTOs
{
    [Table("customers")]
    public class CustomerDTO : BaseModel
    {
        public CustomerDTO()
        {
            // Initialize reference-type properties to empty strings to satisfy non-nullable contracts.
            Name = Phone = Gender = Address = "";

            // Do not hardcode an arbitrary default age; use the type's default (0) so callers explicitly set it when needed.
            Age = default;
        }

        [PrimaryKey("customerID", false)]
        [Column("customerID")]
        public int ID { get; set; }

        [Column("customerName")]
        public string Name { get; set; }
        
        [Column("phoneNumber")]
        public string Phone { get; set; }
        
        [Column("customerGender")]
        public string Gender { get; set; }
        
        [Column("customerAddress")]
        public string Address { get; set; }

        [Column("customerAge")]
        public int? Age { get; set; }
    }

}


