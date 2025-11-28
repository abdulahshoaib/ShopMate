using Npgsql;
using System;

namespace ShopMate.DL
{
    public static class DatabaseHelper
    {

        private const string ConnectionString = "Host=db.aqfwqczfldatbzqlwfqs.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=x9MAjfbTF0gXYuUV";
        public static NpgsqlConnection GetConnection()
        {
            try
            {
                var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DB Connection Failed: {ex.Message}");
                throw;
            }
        }
    }
}