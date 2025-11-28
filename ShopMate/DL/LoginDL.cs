using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Linq;
using System.Threading.Tasks;
using ShopMate.DTOs;

namespace ShopMate.DL
{
    internal class LoginDL
    {
        public async Task<UserDTO?> ValidateLoginAsync(LoginDTO loginDTO)
        {
            var client = SupabaseInitializer.Client;

            // Use strongly-typed .Where() instead of .Filter()
            var response = await client.From<UserDTO>()
                                       .Where(u => u.Username == loginDTO.Username &&
                                                   u.PasswordHash == loginDTO.Password)
                                       .Get();

            var user = response.Models.FirstOrDefault();
            return user;
        }
    }
}
