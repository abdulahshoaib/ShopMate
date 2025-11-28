using Supabase;
using System;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    public static class SupabaseInitializer
    {
        public static Client Client { get; private set; }

        public static async Task InitializeAsync()
        {
            string url = "https://aqfwqczfldatbzqlwfqs.supabase.co"; // Your Supabase URL
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFxZndxY3pmbGRhdGJ6cWx3ZnFzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjQyOTUyMzEsImV4cCI6MjA3OTg3MTIzMX0.L3_o7fYcsOFJt5xgRljP0e9OUIlAtX9DkefjanYqees"; // Your Supabase anon/public key

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            Client = new Client(url, key, options);
            await Client.InitializeAsync();
            Console.WriteLine("✅ Supabase Initialized");
        }
    }
}
