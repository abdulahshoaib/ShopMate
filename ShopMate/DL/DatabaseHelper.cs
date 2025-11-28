using Npgsql;

public static class DatabaseHelper
{
    private const string ConnectionString =
        "Host=db.aqfwqczfldatbzqlwfqs.supabase.co;" +
        "Port=5432;" +
        "Database=postgres;" +
        "Username=postgres;" +
        "Password=x9MAjfbTF0gXYuUV;" +
        "SSL Mode=Require;" +
        "Trust Server Certificate=true;";

    public static NpgsqlConnection GetConnection()
    {
        var conn = new NpgsqlConnection(ConnectionString);
        conn.Open();
        return conn;
    }
}
