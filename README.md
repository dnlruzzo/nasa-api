# nasa-api

This small project implements a very specific call to the Feed endpoint available at NASA NeoWs (Near Earth Object Web Service) RESTful web service.

To use this project you need to generate an api key at https://api.nasa.gov/ and setup the generated api key in your user-secrets as the following:

```
{
  "Nasa": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

## Technologies

* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi)
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [FluentAssertions](https://fluentassertions.com/)

## TODO

* HealthChecks
* Prometheus
* Logging
* Add more tests, both functional and unit tests
* ...
