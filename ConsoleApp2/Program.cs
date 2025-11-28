using Supabase;

namespace SupabaseConnectionTest;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🔄 Testing Supabase connection...");

        string url = "https://aqfwqczfldatbzqlwfqs.supabase.co";
        string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFxZndxY3pmbGRhdGJ6cWx3ZnFzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjQyOTUyMzEsImV4cCI6MjA3OTg3MTIzMX0.L3_o7fYcsOFJt5xgRljP0e9OUIlAtX9DkefjanYqees";

        try
        {
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            var client = new Supabase.Client(url, key, options);

            Console.WriteLine("⏳ Initializing...");
            await client.InitializeAsync();

            Console.WriteLine("✅ Supabase connection SUCCESSFUL!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Supabase connection FAILED!");
            Console.WriteLine("Message: " + ex.Message);

            if (ex.InnerException != null)
                Console.WriteLine("Inner: " + ex.InnerException.Message);
        }
    }
}
