using System.Reflection.Metadata;
using Api.Domain.Common;
using Api.Infrastructure.Core.Contracts;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace WorkerService.Jobs;
public class FetchApiJob : IFetchApiJob
{
    
    protected IJobPrice _jobPrice;
    protected ILogger<FetchApiJob> _logger;
    public FetchApiJob(IJobPrice jobPrice, ILogger<FetchApiJob> logger)
    {
        _jobPrice = jobPrice;
        _logger = logger;
    }
    
    public async Task RunAsync()
    {
        foreach (var symbol in Constants.SYMBOLS)
        {   
            await  _jobPrice.Execute(symbol);
        }
    }

}
