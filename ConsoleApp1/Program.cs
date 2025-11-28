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
            Console.WriteLine("=== Ultra-Minimal Test ===\n");

            AppContext.SetSwitch("System.Net.DisableIPv6", true);

            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback((sender, cert, chain, sslPolicyErrors) => true);

            // BARE MINIMUM connection string
            string connStr = "Host=db.aqfwqczfldatbzqlwfqs.supabase.co;Database=postgres;Username=postgres;Password=shopmate123shopmate;";

            Console.WriteLine($"Testing: {connStr}\n");

            try
            {
                using (var con = new NpgsqlConnection(connStr))
                {
                    con.Open();
                    Console.WriteLine("✅ SUCCESS!");

                    using (var cmd = new NpgsqlCommand("SELECT now();", con))
                    {
                        var result = cmd.ExecuteScalar();
                        Console.WriteLine($"Time: {result}");
                    }
                    con.Close();
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

                Console.WriteLine("\n\n=== TROUBLESHOOTING FOR VIRTUALBOX ===");
                Console.WriteLine("1. Check VirtualBox network adapter settings:");
                Console.WriteLine("   - Settings > Network > Adapter 1 > Attached to: NAT");
                Console.WriteLine("2. Try using Host-Only Adapter instead of NAT");
                Console.WriteLine("3. In VirtualBox: File > Preferences > Network > Check DNS forwarding");
                Console.WriteLine("4. Try pinging 8.8.8.8 in your VM command prompt:");
                Console.WriteLine("   ping 8.8.8.8");
                Console.WriteLine("5. Ensure your firewall allows outbound HTTPS (443) and PostgreSQL (5432)");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}