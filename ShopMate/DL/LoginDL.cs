<<<<<<< HEAD
﻿using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Linq;
using System.Threading.Tasks;
using ShopMate.DTOs;
=======
﻿using Npgsql;
using ShopMate.DTOs; // Ensure you have this namespace/folder created
using System;
using System.Data.SqlTypes;
>>>>>>> 35e9e22d25c513d466883fe8ec25ae9cb75792c2

namespace ShopMate.DL
{
    internal class LoginDL
    {
<<<<<<< HEAD
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
=======
        public UserDTO ValidateLogin(LoginDTO logindto)
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();

                string query = @"
                SELECT u.userID, u.Username, r.roleName
                FROM users u
                JOIN roles r ON r.roleID = u.roleID
                WHERE u.Username = @user
                AND u.passwordHash = @pass";

                using var cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", logindto.Username);
                cmd.Parameters.AddWithValue("@pass", logindto.Password);

                using var reader = cmd.ExecuteReader();
                UserDTO retDTO = new UserDTO();
                while (reader.Read())
                {
                    retDTO.Username = reader["Username"].ToString();
                    retDTO.Id = Convert.ToInt32(reader["userID"]);
                    retDTO.Role = reader["roleName"].ToString();
                    return retDTO;
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return null;
>>>>>>> 35e9e22d25c513d466883fe8ec25ae9cb75792c2
        }
    }
}