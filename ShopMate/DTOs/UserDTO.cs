using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace ShopMate.DTOs
{
    [Table("users")]
    public class UserDTO : BaseModel
    {
        [PrimaryKey("userID", false)]
        public int ID { get; set; }

        [Column("Username")]
        public string Username { get; set; } = string.Empty;

        [Column("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("roleID")]
        public int RoleID { get; set; }

        [Column("employeeID")]
        public int? EmployeeID { get; set; }
    }

}