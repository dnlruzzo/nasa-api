using Nasa.Domain.Constants;
using System.Globalization;

namespace Nasa.Domain.Asteroids;

public class Asteroid
{
    public Asteroid(string? date, float? estimatedDiameterMin, float? estimatedDiameterMax, string? name, float? velocity)
    {
        Date = DateTime.TryParseExact(date, DateFormatConstants.yyyy_MM_dd, CultureInfo.InvariantCulture, DateTimeStyles.None, out _) is false
            ? throw new ArgumentException($"'{nameof(date)}' is not valid.", nameof(date))
            : date;

        ArgumentNullException.ThrowIfNull(estimatedDiameterMin, nameof(estimatedDiameterMin));
        ArgumentNullException.ThrowIfNull(estimatedDiameterMax, nameof(estimatedDiameterMax));
        Diameter = estimatedDiameterMin + estimatedDiameterMax / 2;

        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        Name = name;

        ArgumentNullException.ThrowIfNull(velocity, nameof(velocity));
        Velocity = velocity;
    }

    public string? Date { get; private set; }
    public float? Diameter { get; private set; }
    public string? Name { get; private set; }
    public float? Velocity { get; private set; }
}
