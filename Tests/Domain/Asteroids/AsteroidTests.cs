using Nasa.Domain.Asteroids;

namespace Nasa.Tests.Domain.Asteroids;

public class AsteroidTests
{
    [Fact]
    public void Should_Create_Asteroid()
    {
        var asteroid = new Asteroid(
            date: "2023-01-08",
            estimatedDiameterMin: 0.1460679643f,
            estimatedDiameterMax: 0.3266178974f,
            name: "name",
            velocity: 121198.8588141982f
        );

        asteroid.Should().NotBeNull();
        asteroid.Date.Should().Be("2023-01-08");
        asteroid.Diameter.Should().Be(0.3093769f);
        asteroid.Name.Should().Be("name");
        asteroid.Velocity.Should().Be(121198.8588141982f);
    }

    [Fact]
    public void Throws_ArgumentException_When_Date_IsNotValid()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Asteroid(
            date: "",
            estimatedDiameterMin: 0.1f,
            estimatedDiameterMax: 0.2f,
            name: "name",
            velocity: 40230.2371096415f
        ));

        exception.Message.Should().Be("'date' is not valid. (Parameter 'date')");
    }

    [Fact]
    public void Throws_ArgumentNullException_When_EstimatedDiameterMin_IsNotValid()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Asteroid(
            date: "2023-01-08",
            estimatedDiameterMin: null,
            estimatedDiameterMax: 0.1f,
            name: "name",
            velocity: 40230.2371096415f
        ));

        exception.Message.Should().Be("Value cannot be null. (Parameter 'estimatedDiameterMin')");
    }

    [Fact]
    public void Throws_ArgumentNullException_When_EstimatedDiameterMax_IsNotValid()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Asteroid(
            date: "2023-01-08",
            estimatedDiameterMin: 0.1f,
            estimatedDiameterMax: null,
            name: "name",
            velocity: 40230.2371096415f
        ));

        exception.Message.Should().Be("Value cannot be null. (Parameter 'estimatedDiameterMax')");
    }

    [Fact]
    public void Throws_ArgumentNullException_When_Name_IsNotValid()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Asteroid(
            date: "2023-01-08",
            estimatedDiameterMin: 0.1f,
            estimatedDiameterMax: 0.2f,
            name: null,
            velocity: 40230.2371096415f
        ));

        exception.Message.Should().Be("Value cannot be null. (Parameter 'name')");
    }

    [Fact]
    public void Throws_ArgumentNullException_When_Velocity_IsNotValid()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Asteroid(
            date: "2023-01-08",
            estimatedDiameterMin: 0.1f,
            estimatedDiameterMax: 0.1f,
            name: "name",
            velocity: null
        ));

        exception.Message.Should().Be("Value cannot be null. (Parameter 'velocity')");
    }
}
