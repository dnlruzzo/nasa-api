namespace Nasa.Domain.Settings;

public class NasaSettings
{
    public const string FeedHttpClientBaseAddress = "https://api.nasa.gov/neo/rest/v1/feed";

    public const string SectionName = "Nasa";

    public string? ApiKey { get; init; }
}
