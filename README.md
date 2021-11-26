# CSharpDevelopmentTestTSS

## Technologies

* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)

## Getting Started

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Navigate to `src/Core.Api` and run `dotnet run` to launch the back end (ASP.NET Core Web API)

### Database Configuration

The project is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **Core.Api/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": true,
```

