using Api.Infrastructure.Core.Contracts;
using Api.Infrastructure.Core.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Core
{
    public static class WebApiServiceExtension
    {
        public static void AddWebApiService(this IServiceCollection services, IConfigurationRoot Configuration, String assemblyName)
        {
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<CryptoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CryptoContext"),
                                    sqlOptions => sqlOptions.MigrationsAssembly(assemblyName));
            },
            ServiceLifetime.Scoped // Note that Scoped is the default choice
                                    // in AddDbContext. It is shown here only for
                                    // pedagogic purposes.
            );

            services.AddScoped<IPairPriceRepository, PairPriceRepository>();
        }
    }
}