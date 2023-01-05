using Microsoft.Extensions.DependencyInjection;
using Nasa.Infrastructure.HttpClients.NeoWs;

namespace Nasa.Tests.Infrastructure.HttpClients.NeoWs;

public class FeedHttpClientTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public FeedHttpClientTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_Get_Feed()
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var client = scope.ServiceProvider.GetRequiredService<FeedHttpClient>();

        var response = await client.GetAsync(days: 1, default);

        response.Should().NotBeNull();
    }

    [Theory]
    [InlineData(8, HttpStatusCode.BadRequest)]
    public async Task CanNotGetFeed(int days, HttpStatusCode statusCode)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var client = scope.ServiceProvider.GetRequiredService<FeedHttpClient>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await client.GetAsync(days, default));

        exception.StatusCode.Should().Be(statusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CanGetFeed(int days)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var client = scope.ServiceProvider.GetRequiredService<FeedHttpClient>();

        var response = await client.GetAsync(days, default);

        response.Should().NotBeNull();
    }
}
