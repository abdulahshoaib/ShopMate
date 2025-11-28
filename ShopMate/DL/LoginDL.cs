using Npgsql;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    internal class LoginDL
    {
        private LoginDTO logindto;
        public UserDTO ValidateLogin(LoginDTO logindto)
        {
            using var con = ConnectionHelper.GetConnection();
            con.Open();

            string query = @"
                SELECT u.id, u.username, r.rolename
                FROM users u
                JOIN roles r ON r.id = u.roleid
                WHERE u.username = @user
                AND u.passwordhash = @pass
                LIMIT 1;
            ";

            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", logindto.Username);
            cmd.Parameters.AddWithValue("@pass", logindto.Password);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new UserDTO
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Role = reader.GetString(2)
                };
            }

            return null;
        }
    }
}
