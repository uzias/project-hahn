using System.Reflection;
using Api.Domain.Common;
using Api.Infrastructure.Core.Contracts;
using Api.Infrastructure.Core.Repository;
using Api.Infrastructure.Core.Repository.Context;
using Api.Infrastructure.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Api.Infrastructure.Core
{
    public static class WorkerServiceExtension
    {
        public static IServiceCollection AddWorkerService(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddHttpClient(Constants.API_NAME, (serviceProvider,httpClient) =>
            {
                var settings = GetAppSettings(serviceProvider);

                // using Microsoft.Net.Http.Headers;
                // The GitHub API requires two headers.
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.Accept, "application/json; charset=utf-8");

                httpClient.DefaultRequestHeaders.Add(
                    Constants.HEADER_KEY_NAME, settings.Key);
            });
            services.AddScoped<IAvgPriceRepository, AvgPriceRepository>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IJobPrice, JobPrice>();
            services.AddScoped<IPairPriceRepository, PairPriceRepository>();
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<CryptoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CryptoContext"),
                                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(WorkerServiceExtension).GetTypeInfo().
                                                                                        Assembly.GetName().Name));
            },
            ServiceLifetime.Scoped // Note that Scoped is the default choice
                                    // in AddDbContext. It is shown here only for
                                    // pedagogic purposes.
            );
            return services;
        }

        public static WorkerServiceSettings GetAppSettings(IServiceProvider serviceProvider){
            var appSettings = serviceProvider.GetService<IOptions<WorkerServiceSettings>>()?.Value;
                
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            return appSettings;
        }
    }
}