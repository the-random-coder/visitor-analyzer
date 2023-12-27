namespace TheRandomCoder.VisitorAnalyzer;

public static class Storage
{
    private const string query = "INSERT INTO [dbo].[IpAddressLookups] ([IpAddress] ,[Date],[CountryName],[CountryCode],[City] ,[Region],[RegionCode],[Continent] ,[Latitude] ,[Longitude]) VALUES(@ipAddress, @date, @countryName, @countryCode, @city, @region, @regionCode, @continent, @latitude, @longitude )";

    private static readonly string? _connectionString = Environment.GetEnvironmentVariable("STORAGE_DATABASE");

    public static async Task SaveResultAsync(string ipAddress, Result data)
    {
        try
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            var parameters = new SqlParameter[]
            {
                new("@ipAddress", ipAddress),
                new("@date", DateTime.UtcNow.Date),
                new("@countryName", data.CountryName),
                new("@countryCode", data.CountryCode),
                new("@city", data.City),
                new("@region", data.Region),
                new("@regionCode", data.RegionCode),
                new("@continent", data.Continent),
                new("@latitude", data.Latitude),
                new("@longitude", data.Longitude),
            };

            command.Parameters.AddRange(parameters);
            connection.Open();
            int result = await command.ExecuteNonQueryAsync();
        }
        catch (Exception) { }
    }
}