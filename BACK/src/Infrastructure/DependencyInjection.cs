using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "InMemoryDb");
            });
            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
            services.AddScoped<IJwtGenerator, JwtGenerator>();


            return services;
        }
    }
}
