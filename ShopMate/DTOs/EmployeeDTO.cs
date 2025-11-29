using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ShopMate.DTOs
{
    [Table("salesPersons")]
    public class EmployeeDTO : BaseModel
    {
        [PrimaryKey("salesPersonID", false)]
        [Column("salesPersonID")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("phoneNumber")]
        public string Phone { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        public EmployeeDTO()
        {
        }
    }
}