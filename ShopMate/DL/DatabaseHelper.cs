using Npgsql;

public static class DatabaseHelper
{
    [System.Obsolete]
    public static NpgsqlConnection GetConnection()
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = "cflkjtxddlplfejixnnc.db.eu-west-2.nhost.run",
            Port = 5432,
            Username = "postgres",
            Password = "eTbF5GS3U9aHe@T",
            Database = "cflkjtxddlplfejixnnc",
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };

        var conn = new NpgsqlConnection(builder.ConnectionString);
        conn.Open();
        return conn;
    }
}
