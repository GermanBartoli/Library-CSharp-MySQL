namespace BitsionFicticiaSA.Models;

public static class GestorBDConexion
{
    private static string conexionString = "";

    public static string ConexionString { get => conexionString; set => conexionString = value; }
}