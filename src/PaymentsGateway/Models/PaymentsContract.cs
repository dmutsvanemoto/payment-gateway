using System.Text.Json.Serialization;

namespace PaymentsGateway.Models
{
    public class PaymentsContract
    {
        [JsonPropertyName("paymentId")]
        public int PaymentId { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
