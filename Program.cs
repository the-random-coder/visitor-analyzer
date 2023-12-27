var requiredApiKey = Environment.GetEnvironmentVariable("API_KEY")!;
var app = Setup.CreateApp(args);

app.MapGet("/{ipAddress}", async (IMemoryCache cache, string ipAddress, [FromQuery] string apiKey, [FromQuery] bool store = false) =>
{
    // check if the request has provided an api key
    if (string.IsNullOrEmpty(apiKey)) return Results.Unauthorized();
    if (apiKey != requiredApiKey) return Results.Unauthorized();

    // check if the check contains the ipAddress
    var cached = cache.Get<Result>(ipAddress);
    if (cached != null) return Results.Json(cached);

    // lookup the ip address
    var result = Resolver.Lookup(ipAddress);
    if (result == null) return Results.NotFound();

    // Check if the results needs to be stored
    if (store) await Storage.SaveResultAsync(ipAddress, result);
    cache.Set(ipAddress, result, DateTimeOffset.UtcNow.AddHours(12));
    return Results.Json(result);
});

app.Run();