using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HotelSearch.Core.Interfaces;
using HotelSearch.Infrastructure.Repositories;
using HotelSearch.Infrastructure.Data;
using HotelSearch.Infrastructure.ExternalServices;
using HotelSearch.Core.Entities;

namespace HotelSearch.Infrastructure.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Register DbContext with SQL Server or In-Memory database as required
            services.AddDbContext<HotelDbContext>(options =>
                options.UseInMemoryDatabase("HotelDb"));

            // Register repositories
            services.AddScoped<IRepository<Hotel>, HotelRepository>();

            // Register external services
            services.AddSingleton<GeoLocationService>();

            return services;
        }
    }
}
