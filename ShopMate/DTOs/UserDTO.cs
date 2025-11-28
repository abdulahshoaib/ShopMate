using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("users")]
public class UserDTO : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

<<<<<<< HEAD
    [Column("username")]
    public string Username { get; set; }

    [Column("passwordhash")]
    public string PasswordHash { get; set; }

    [Column("role")]
    public string Role { get; set; }
=======
namespace ShopMate.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
>>>>>>> 35e9e22d25c513d466883fe8ec25ae9cb75792c2
}
