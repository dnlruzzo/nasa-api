namespace Nasa.Tests.Api.Endpoints.Asteroids;

public class GetAsteroidsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GetAsteroidsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(0, HttpStatusCode.BadRequest)]
    [InlineData(-1, HttpStatusCode.BadRequest)]
    [InlineData(1, HttpStatusCode.OK)]
    [InlineData(8, HttpStatusCode.BadRequest)]
    [InlineData(null, HttpStatusCode.BadRequest)]
    public async Task CanGetAsteroids(int? days, HttpStatusCode statusCode)
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync(days is null ? "/asteroids" : $"/asteroids?days={days}");
        response.StatusCode.Should().Be(statusCode);
    }
}
