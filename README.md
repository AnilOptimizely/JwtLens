# JwtLens

JwtLens captures inbound and outbound JWT bearer tokens in ASP.NET Core apps and surfaces local diagnostics such as claims, expiry, and algorithm warnings. It is part of the [Lens family for .NET](https://github.com/AnilOptimizely/HttpLens/blob/main/docs/lens-family-architecture.md).

## Install

```shell
dotnet add package JwtLens
```

## Minimal setup

```csharp
using JwtLens.Extensions;
using JwtLens.Interceptors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtLens();

// Optional: capture tokens sent by outbound HttpClient calls.
builder.Services.AddHttpClient("outbound").AddHttpMessageHandler<JwtLensDelegatingHandler>();

var app = builder.Build();

app.UseJwtLens();

app.Run();
```

To enable JwtLens only in specific environments, use the `AddJwtLens(builder.Environment, options => ...)` overload and set `JwtLensOptions.AllowedEnvironments`.

## Repository layout

| Path | Purpose |
|---|---|
| `src/JwtLens` | The `JwtLens` NuGet package. |
| `tests/JwtLens.Tests` | Unit tests. |
| `samples/JwtLens.Sample` | Minimal app showing the `AddJwtLens`/`UseJwtLens` setup. |
| `docs/issues` | JwtLens design history. |

## Building

```shell
dotnet restore
dotnet build
dotnet test
```

The SDK is pinned in `global.json`. Tests run on net9.0 and net10.0, so install those runtimes as well.

## Dependencies and compatibility

- JwtLens depends on the shared [`HttpLens.Abstractions`](https://www.nuget.org/packages/HttpLens.Abstractions) package (namespace `Lens.Abstractions`), which lives in the [HttpLens repository](https://github.com/AnilOptimizely/HttpLens/tree/main/src/Lens.Abstractions).
- HttpLens owns the compatibility suite that verifies HttpLens and JwtLens work together in one app.
- Family-wide conventions (naming, versioning, shared contracts) are documented in the HttpLens repository: [architecture](https://github.com/AnilOptimizely/HttpLens/blob/main/docs/lens-family-architecture.md) and [package IDs](https://github.com/AnilOptimizely/HttpLens/blob/main/docs/package-id-decisions.md).

## Releasing

1. Update `<Version>` in `src/JwtLens/JwtLens.csproj`.
2. Push a matching tag, for example `v0.2.0`. The release workflow checks that the tag matches the version, runs the tests, and pushes only the JwtLens package to NuGet. It publishes with [NuGet Trusted Publishing](https://learn.microsoft.com/nuget/nuget-org/trusted-publishing): it needs a nuget.org trusted publishing policy for this repository and `release.yml`. No API key is stored.

## License

[MIT](LICENSE)
