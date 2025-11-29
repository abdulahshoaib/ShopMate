using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ShopMate.DTOs
{
    [Table("customers")]
    public class CustomerDTO : BaseModel
    {
        public CustomerDTO()
        {
            Name = Phone = Gender = Address = "";
            Age = 5;
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
        public int Age { get; set; }
    }

}


