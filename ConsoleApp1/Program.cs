using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SupabaseConnectionTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Direct TCP Connection Test (Bypasses Npgsql) ===\n");

            string host = "db.aqfwqczfldatbzqlwfqs.supabase.co";
            int port = 5432;

            Console.WriteLine($"🔹 Testing TCP connection to {host}:{port}\n");

            try
            {
                using (var client = new TcpClient())
                {
                    // 30-second timeout
                    client.SendTimeout = 30000;
                    client.ReceiveTimeout = 30000;

                    Console.WriteLine("Connecting...");
                    await client.ConnectAsync(host, port);
                    Console.WriteLine("✅ TCP Connection successful!\n");

                    // If we got here, the network is working
                    Console.WriteLine("🎉 Network can reach Supabase!\n");
                    Console.WriteLine("The issue is NOT network-related.");
                    Console.WriteLine("Try updating Npgsql NuGet package to latest version.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ TCP Connection failed:\n");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Type: {ex.GetType().Name}\n");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner: {ex.InnerException.Message}\n");
                }

                Console.WriteLine("❌ Your network CANNOT reach Supabase.");
                Console.WriteLine("Check:");
                Console.WriteLine("  1. Firewall rules (port 5432 must be open)");
                Console.WriteLine("  2. VirtualBox network settings");
                Console.WriteLine("  3. Try restarting the VM");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}