namespace Library.Models;

public static class DBMConnection
{
    private static string connectionString = "";

    public static string ConnectionString { get => connectionString; set => connectionString = value; }
}
