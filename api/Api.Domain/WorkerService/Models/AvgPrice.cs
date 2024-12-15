using System.Text.Json.Serialization;

namespace Api.Domain.WorkerService.Models
{
    public class AvgPrice
    {
        [JsonPropertyName("mins")]
        public int Mins { get; set; }
        [JsonPropertyName("price")]
        public required string Price { get; set; }
        [JsonPropertyName("closedTime")]
        public decimal ClosedTime { get; set; }
    }
}