using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nasa.Application.Asteroids.Requests.GetAsteroids;

namespace Nasa.Tests.Application.Asteroids.Requests;

public class GetAsteroidsRequestTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GetAsteroidsRequestTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(8)]
    [InlineData(null)]
    public async Task CanGetAsteroidsTheory(int? days)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await mediator.Send(new GetAsteroidsRequest(days)));

        if (days is null)
        {
            exception.Message.Should().Be("Validation failed: \r\n -- Days: 'Days' must not be empty. Severity: Error");
        }
        else
        {
            exception.Message.Should().Be($"Validation failed: \r\n -- Days: 'Days' must be between 1 and 7. You entered {days}. Severity: Error");
        }
    }

    [Theory]
    [InlineData(7)]
    public async Task CanGetAsteroids(int? days)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var response = await mediator.Send(new GetAsteroidsRequest(days));

        response.Should().NotBeNull();
    }
}
