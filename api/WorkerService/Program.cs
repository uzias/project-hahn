using WorkerService;
using Hangfire;
using Api.Domain.Common;
using Api.Infrastructure.Core;
using Hangfire.SqlServer;
using Api.Infrastructure.Core.Contracts;
using WorkerService.Jobs;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


// read settings from appsettings.json
var settings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.Configure<WorkerServiceSettings>(settings.GetSection("Api"));
builder.Services.AddWorkerService(settings);
builder.Services.AddScoped<IFetchApiJob, FetchApiJob>();

builder.Services.AddHangfire((serviceProvider, configuration) =>
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(settings.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    })
);

builder.Services.AddHangfireServer();

var host = builder.Build();
host.Run();
