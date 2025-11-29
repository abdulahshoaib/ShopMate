using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.DTOs
{
    [Supabase.Postgrest.Attributes.Table("users")]
    public class UserDTO : BaseModel
    {
        [PrimaryKey("userID", false)]
        public int UserID { get; set; }

        [Supabase.Postgrest.Attributes.Column("Username")]
        public string Username { get; set; } = string.Empty;

        [Supabase.Postgrest.Attributes.Column("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Supabase.Postgrest.Attributes.Column("roleID")]
        public int RoleID { get; set; }

    }
}
