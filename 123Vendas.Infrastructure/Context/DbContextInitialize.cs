using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace _123Vendas.Infrastructure.Context
{
    public static class DbContextInitialize
    {
        public static IServiceCollection AddDataBaseSqlServer(this IServiceCollection services, string connectionString)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<SalesDbContext>(options =>
                options.UseSqlServer(connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                }));

            return services;
        }
    }
}
