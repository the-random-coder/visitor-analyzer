using MaxMind.GeoIP2;

namespace TheRandomCoder.VisitorAnalyzer;

public class Resolver
{
    private static readonly string FileLocation = Environment.GetEnvironmentVariable("DATABASE_PATH")!;

    private readonly static DatabaseReader _reader = new(FileLocation);

    public static async Task<Result?> Lookup(string ipAddress)
    {
        if (!File.Exists(FileLocation)) await Updater.Update();
        var result = _reader.City(ipAddress);
        if (result == null) return null;

        return new()
        {
            Continent = result.Continent.Name,
            CountryCode = result.Country.IsoCode,
            CountryName = result.Country.Name,
            City = result.City.Name,
            Latitude = result.Location.Latitude,
            Longitude = result.Location.Longitude,
            Region = result.MostSpecificSubdivision.Name,
            RegionCode = result.MostSpecificSubdivision.IsoCode
        };
    }
}