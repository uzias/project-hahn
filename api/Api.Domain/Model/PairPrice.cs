namespace Api.Domain.Common.Model
{
    public class PairPrice
    {
        public int Mins { get; set; }
        public required string Price { get; set; }
        public required string Symbol { get; set; }
    }
}