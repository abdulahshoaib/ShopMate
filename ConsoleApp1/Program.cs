using Npgsql;
using System;
using System.Net;
using System.Net.Security;

namespace SupabaseConnectionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Supabase Connection Diagnostic (FINAL FIX) ===\n");

            // CRITICAL FOR VIRTUALBOX
            AppContext.SetSwitch("System.Net.DisableIPv6", true);
            AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2Support", false);

            // Bypass SSL
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback((sender, cert, chain, sslPolicyErrors) => true);

            ServicePointManager.DefaultConnectionLimit = 10;

            // CORRECTED: Use "Timeout" not "Connection Timeout"
            string connStr = "Host=db.aqfwqczfldatbzqlwfqs.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=shopmate123shopmate;SslMode=Require;Pooling=false;Timeout=30;";

            Console.WriteLine($"🔹 Testing connection string:\n{connStr}\n");

            try
            {
                using (var con = new NpgsqlConnection(connStr))
                {
                    Console.WriteLine("Attempting to open connection...");
                    con.Open();
                    Console.WriteLine("✅ Connection opened!\n");

                    using (var cmd = new NpgsqlCommand("SELECT now();", con))
                    {
                        var result = cmd.ExecuteScalar();
                        Console.WriteLine($"✅ Query executed!\n🕒 Server time: {result}\n");
                    }

                    con.Close();
                    Console.WriteLine("🎉 SUCCESS! Connection is working!\n");
                    Console.WriteLine($"✅ Use this connection string in your code:\n{connStr}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Connection failed:\n");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Type: {ex.GetType().Name}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                Console.WriteLine($"\nStack Trace:\n{ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}