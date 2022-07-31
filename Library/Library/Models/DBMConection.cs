namespace Library.Models;

public static class DBMConection
{
    private static string conectionString = "";

    public static string ConectionString { get => conectionString; set => conectionString = value; }
}