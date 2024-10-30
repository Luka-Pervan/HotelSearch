using HotelSearch.Application.Interfaces;
using HotelSearch.Application.Services;
using HotelSearch.Infrastructure.Configurations;
using HotelSearch.Infrastructure.Data;
using HotelSearch.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Services
// - Adds controllers with JSON serialization
// - Configures Swagger for API documentation
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Add Application Layer Services
// - Configures AutoMapper with mapping profiles in the Application layer
builder.Services.AddAutoMapper(typeof(HotelMappingProfile));

// 3. Add Infrastructure Layer Services
// - Adds DbContext with in-memory database (switchable to SQL Server if needed)
// - Registers repositories, services, and other dependencies for DI
builder.Services.AddInfrastructureServices("InMemoryDbConnectionString");

// 4. Dependency Injection for Application Services
// - Registers IHotelService implementation for DI
builder.Services.AddScoped<IHotelService, HotelService>();

// 5. Database Configuration
// - Configures the application to use an in-memory database for this example
builder.Services.AddDbContext<HotelDbContext>(options =>
{
    options.UseInMemoryDatabase("HotelDb");
});

var app = builder.Build();

// 6. Configure Middleware
// - Swagger middleware for API documentation
// - Error handling middleware (simplified)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 7. Map Controllers and Start Application
app.MapControllers();

app.Run();
