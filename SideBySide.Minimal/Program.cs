
using SideBySide.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var openApiDocumentName = "weather-forecasts-minimal";
var openApiDocumentVersion = "2021-07-01";
var openApiDocumentTitle = "SideBySide.Minimal";

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(openApiDocumentName, new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = openApiDocumentTitle,
        Version = openApiDocumentVersion
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/{openApiDocumentName}/swagger.json", openApiDocumentName);
    });
}

app.MapGet("/", () =>
{
    return WeatherForecast.GetRandomForecast(7);
});

app.Run();
