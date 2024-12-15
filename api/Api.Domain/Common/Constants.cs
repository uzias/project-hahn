namespace Api.Domain.Common
{
    public static class Constants
    {
        public const string API_NAME = "Crypto API";
        public const string HEADER_KEY_NAME = "X-MBX-APIKEY";

        public static readonly List<string> SYMBOLS = new()
        {
            "BTCUSDT",
            "ETHUSDT",
            "BNBUSDT",
            "ADAUSDT",
            "XRPUSDT",
            "DOGEUSDT",
            "DOTUSDT",
            "UNIUSDT",
        };
    }
}