namespace QuantityMeasurementRepoLayer.Config;

public static class DatabaseConfig
{
    private static readonly string ConnectionString = $"Server=localhost;Database=QuantityMeasurementDB;Integrated Security=True;TrustServerCertificate=True;";
    public static string GetConnectionString()
    {
        return ConnectionString;
    }
}