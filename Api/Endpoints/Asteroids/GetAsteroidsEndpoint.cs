using Nasa.Application.Asteroids.Requests.GetAsteroids;

namespace Microsoft.AspNetCore.Builder;

public static class GetAsteroidsEndpoint
{
    public static RouteHandlerBuilder MapGetAsteroidsEndpoint(this IEndpointRouteBuilder builder)
    {
        var endpoint = builder.MapGet("/asteroids", async (int? days, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var request = new GetAsteroidsRequest(days);
            var response = await mediator.Send(request, cancellationToken);
            return Results.Ok(response);
        })
        .WithName("GetAsteroids")
        .WithOpenApi();

        return endpoint;
    }
}
