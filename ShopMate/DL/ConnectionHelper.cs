using System.Data;
using Npgsql;

namespace ShopMate.DL
{
    public static class ConnectionHelper
    {
        private static string connectionString =
                "Host=db.aqfwqczfldatbzqlwfqs.supabase.co;" +
                "Port=5432;" +
                "Database=postgres;" +
                "Username=postgres;" +
                "shopmate123shopmate;" +
                "SSL Mode=Disable;" +
                "Trust Server Certificate=true;";


        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
