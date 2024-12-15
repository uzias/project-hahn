namespace Api.Infrastructure.Core.Contracts
{
    public interface IJobPrice
    {
        Task Execute(String symbol);
    }
}