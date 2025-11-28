using System.Data;
using Npgsql;

namespace ShopMate.DL
{
    public static class ConnectionHelper
    {
        // 🔥 CHANGE THIS ONLY — every contributor will use same DB
        private static string connectionString =
            "{\r\n  \"ConnectionStrings\": {\r\n    \"DefaultConnection\": \"Host=db.aqfwqczfldatbzqlwfqs.supabase.co;Database=postgres;Username=postgres;Password=!@#123456!@#;SSL Mode=Require;Trust Server Certificate=true\"\r\n  }\r\n}";

        public static NpgsqlConnection GetConnection()
        {
            var con = new NpgsqlConnection(connectionString);
            return con;
        }

        // Optional: for pooling or checking
        public static bool TestConnection()
        {
            try
            {
                using (var con = GetConnection())
                {
                    con.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
