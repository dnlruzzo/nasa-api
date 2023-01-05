using FluentValidation;
using MediatR;
using Nasa.Domain.Asteroids;
using Nasa.Infrastructure.HttpClients.NeoWs;
using Nasa.Infrastructure.HttpClients.NeoWs.Models;

namespace Nasa.Application.Asteroids.Requests.GetAsteroids;

public class GetAsteroidsRequest : IRequest<IReadOnlyCollection<Asteroid>?>
{
    public GetAsteroidsRequest(int? days)
    {
        Days = days;
    }

    public int? Days { get; init; }
}

public class GetAsteroidsRequestHandler : IRequestHandler<GetAsteroidsRequest, IReadOnlyCollection<Asteroid>?>
{
    private readonly FeedHttpClient _client;

    public GetAsteroidsRequestHandler(FeedHttpClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<Asteroid>?> Handle(GetAsteroidsRequest request, CancellationToken cancellationToken)
    {
        if (await _client.GetAsync(request.Days!.Value, cancellationToken) is Feed feed)
        {
            return feed.NearEarthObjects.SelectMany(x => x.Value).Where(x => x.IsPotentiallyHazardousAsteroid).Select(x =>
            {
                var closeApproachData = x.CloseApproachData
                    .Where(x => !string.IsNullOrWhiteSpace(x.CloseApproachDate))
                    .OrderBy(x => x.CloseApproachDate)
                    .FirstOrDefault();

                var asteroid = new Asteroid(
                    date: closeApproachData?.CloseApproachDate,
                    estimatedDiameterMin: x.EstimatedDiameter?.Kilometers?.EstimatedDiameterMin,
                    estimatedDiameterMax: x.EstimatedDiameter?.Kilometers?.EstimatedDiameterMax,
                    name: x.Name,
                    velocity: closeApproachData?.RelativeVelocity?.KilometersPerHour
                );

                return asteroid;
            })
            .OrderByDescending(x => x.Diameter)
            .Skip(0)
            .Take(3)
            .ToHashSet();
        }

        return default;
    }
}

public class GetAsteroidsRequestValidator : AbstractValidator<GetAsteroidsRequest>
{
    public GetAsteroidsRequestValidator()
    {
        RuleFor(x => x.Days)
            .NotNull()
            .InclusiveBetween(1, 7);
    }
}
