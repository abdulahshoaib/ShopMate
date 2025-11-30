using ShopMate.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    internal class LoginDL
    {
        public async Task<UserDTO?> ValidateLoginAsync(LoginDTO loginDTO)
        {
            var client = SupabaseInitializer.client;

            string hashed = HashPassword(loginDTO.Password);

            // Fetch user by username and password
            var response = await client.From<UserDTO>()
                                       .Where(u => u.Username == loginDTO.Username &&
                                                   u.PasswordHash == hashed)
                                       .Get();

            var user = response.Models.FirstOrDefault();

            return user;
        }
        private string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
