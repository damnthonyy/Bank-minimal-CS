using Application.Interfaces;
using Bank.Infrastructure.Repositories;
using Bank.Application.UseCases;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add endpoints and swagger gen ( with swashbuckle.aspnetcore)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Bank API",
        Description = "An ASP.NET Core Web API for managing Bank items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
builder.Services.AddTransient<AccountUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = false; // Use OpenApi v3
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Bank API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World");

app.Run();
