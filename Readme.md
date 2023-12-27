# Visitor Analyzer

This is a simple wrapper for the GeoLite2 MaxMind City database. This API will ensure the database is downloaded and no information is transmitted to a third-party, so this solution is GDPR compliant!

The website service is existing the follow environment variables to be set

| Variable         | Description                                                           |
|------------------|-----------------------------------------------------------------------|
| API_KEY          | This API key will be used by your clients connecting with the service |
| MAXMIND_LICENSE  | The license key for downloading the GeoLite2 City database            |
| DATABASE_PATH    | The location to store the GeoLite2 City database                      |
| STORAGE_DATABASE | Connection string to store lookups                                    |

Please note: STORAGE_DATABASE is optional. But it can be set for caching lookup for future analysis. It is not required for internal caching, since a memory cache will be used for that.