namespace PaymentsAPI.Data.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int MerchantId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
