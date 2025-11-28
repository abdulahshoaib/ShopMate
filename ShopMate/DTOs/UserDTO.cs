using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("users")]
public class UserDTO : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("passwordhash")]
    public string PasswordHash { get; set; }

    [Column("role")]
    public string Role { get; set; }
}
