using JwtLens.Extensions;
using JwtLens.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtLens();

var app = builder.Build();

app.UseJwtLens();

app.MapGet("/", () => "JwtLens sample. Call /api/ping with an 'Authorization: Bearer <jwt>' header, then GET /jwtlens/events.");
app.MapGet("/api/ping", () => Results.Ok(new { message = "pong" }));
app.MapGet("/jwtlens/events", (IJwtEventStore store) => Results.Ok(store.GetAll()));

app.Run();
