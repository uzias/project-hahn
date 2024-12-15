namespace Api.Infrastructure.Core.Contracts
{
    public interface IFetchApiJob
    {
       public Task RunAsync();
    }
}