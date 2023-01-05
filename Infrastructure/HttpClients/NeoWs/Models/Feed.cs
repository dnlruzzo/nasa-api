using System.Text.Json.Serialization;

namespace Nasa.Infrastructure.HttpClients.NeoWs.Models;

public class Feed
{
    [JsonPropertyName("near_earth_objects")]
    public IReadOnlyDictionary<string, List<NearEarthObject>> NearEarthObjects { get; init; } = new Dictionary<string, List<NearEarthObject>>();
}

public class NearEarthObject
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("estimated_diameter")]
    public EstimatedDiameter? EstimatedDiameter { get; init; }

    [JsonPropertyName("is_potentially_hazardous_asteroid")]
    public bool IsPotentiallyHazardousAsteroid { get; init; }

    [JsonPropertyName("close_approach_data")]
    public IReadOnlyCollection<CloseApproachData> CloseApproachData { get; init; } = new List<CloseApproachData>();
}

public class EstimatedDiameter
{
    [JsonPropertyName("kilometers")]
    public Kilometers? Kilometers { get; init; }
}

public class Kilometers
{
    [JsonPropertyName("estimated_diameter_min")]
    public float? EstimatedDiameterMin { get; init; }

    [JsonPropertyName("estimated_diameter_max")]
    public float? EstimatedDiameterMax { get; init; }
}

public class CloseApproachData
{
    [JsonPropertyName("close_approach_date")]
    public string? CloseApproachDate { get; init; }

    [JsonPropertyName("relative_velocity")]
    public RelativeVelocity? RelativeVelocity { get; init; }
}

public class RelativeVelocity
{
    [JsonPropertyName("kilometers_per_hour")]
    public float? KilometersPerHour { get; init; }
}
