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

app.MapGet("/testOutputFormatting", (HttpContext httpContext) =>
{
    return httpContext.Request.Method;
});

app.MapPut("/put", (string id,
                    WeatherForecast weatherForecast,
                    HttpContext httpContext) =>
{
    return httpContext.Request.Method;
});

app.MapPost("/post", (WeatherForecast weatherForecast) =>
{
    return weatherForecast;
});

//app.MapPost("/upload", async (IFormFile file, CancellationToken token) =>
//{
//    using (var stream = System.IO.File.OpenWrite("upload.txt"))
//    {
//        await file.CopyToAsync(stream);
//    }
//});

app.MapPost("/upload", async (HttpRequest req) =>
{
    if (!req.HasFormContentType)
    {
        return Results.BadRequest();
    }

    var form = await req.ReadFormAsync();
    var file = form.Files["file"];

    if (file is null)
    {
        return Results.BadRequest();
    }

    var uploads = file.FileName;
    using var fileStream = File.OpenWrite(uploads);
    using var uploadStream = file.OpenReadStream();
    await uploadStream.CopyToAsync(fileStream);

    return Results.NoContent();
});

app.Run();
