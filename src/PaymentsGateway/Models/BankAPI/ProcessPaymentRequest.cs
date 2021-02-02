namespace PaymentsGateway.Models.BankAPI
{
    public class ProcessPaymentRequest
    {
        public int MerchantId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public CardDetails CardDetails { get; set; }
    }
}