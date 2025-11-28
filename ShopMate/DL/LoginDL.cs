using Npgsql;
using ShopMate.DTOs; // Ensure you have this namespace/folder created
using System;
using System.Data.SqlTypes;

namespace ShopMate.DL
{
    internal class LoginDL
    {
        public UserDTO ValidateLogin(LoginDTO logindto)
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();

                string query = "SELECT \"userID\", \"Username\", \"roleID\", \"passwordHash\" FROM users WHERE \"Username\" = @user AND \"passwordHash\" = @pass";

                using var cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", logindto.Username);
                cmd.Parameters.AddWithValue("@pass", logindto.Password);

                using var reader = cmd.ExecuteReader();
                UserDTO retDTO = new UserDTO();
                while (reader.Read())
                {
                    retDTO.Username = reader["Username"].ToString();
                    retDTO.Id = Convert.ToInt32(reader["userID"]);
                    retDTO.Role = Convert.ToInt32(reader["roleID"]);
                    return retDTO;
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return null;
        }
    }
}