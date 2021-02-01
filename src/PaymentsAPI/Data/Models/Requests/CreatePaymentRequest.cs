namespace PaymentsAPI.Data.Models.Requests
{
    public class CreatePaymentRequest
    {
        public int MerchantId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
