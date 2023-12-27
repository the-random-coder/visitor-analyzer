using System.Formats.Tar;
using System.IO.Compression;

namespace TheRandomCoder.VisitorAnalyzer;

public class Updater
{
    private static readonly string? LicenseKey = Environment.GetEnvironmentVariable("MAXMIND_LICENSE");

    private static readonly string FileLocation = Environment.GetEnvironmentVariable("DATABASE_PATH")!;

    public static async Task Update()
    {
        try
        {
            // Download and extract the city database
            var uri = $"https://download.maxmind.com/app/geoip_download?edition_id=GeoLite2-City&license_key={LicenseKey}&suffix=tar.gz";
            var client = new HttpClient();
            using var downloadStream = await client.GetStreamAsync(uri);
            using var gzipStream = new GZipStream(downloadStream, CompressionMode.Decompress);
            var tempDirectory = GetTemporaryDirectory();
            await TarFile.ExtractToDirectoryAsync(gzipStream, tempDirectory, true);

            // The tar file contains a data specific folder
            var database = Directory.GetFiles(tempDirectory, "*.mmdb", SearchOption.AllDirectories).First();
            File.Copy(database, FileLocation, true);
            Directory.Delete(tempDirectory, true);
        }
        catch (Exception) { }
    }

    private static string GetTemporaryDirectory()
    {
        string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(tempDirectory);
        return tempDirectory;
    }
}