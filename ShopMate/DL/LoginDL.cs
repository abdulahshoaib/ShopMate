using ShopMate.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Supabase.Postgrest;

namespace ShopMate.DL
{
    internal class LoginDL
    {
        public async Task<UserDTO?> ValidateLoginAsync(LoginDTO loginDTO)
        {
            var client = SupabaseInitializer.Client;

            // Fetch user by username and password
            var response = await client.From<UserDTO>()
                                       .Where(u => u.Username == loginDTO.Username &&
                                                   u.PasswordHash == loginDTO.Password)
                                       .Get();

            var user = response.Models.FirstOrDefault();

            return user;
        }
    }
}
