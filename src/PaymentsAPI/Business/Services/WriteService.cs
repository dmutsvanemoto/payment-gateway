using System.Threading.Tasks;
using PaymentsAPI.Data;
using PaymentsAPI.Data.Models;
using PaymentsAPI.Data.Models.Requests;

namespace PaymentsAPI.Business.Services
{
    public class WriteService : IWriteService
    {
        private readonly PaymentsDbContext _context;

        public WriteService(PaymentsDbContext context)
        {
            _context = context;
        }


        public async Task CreatePayment(CreatePaymentRequest createPaymentRequest)
        {
            var payment = new Payment
            {
                MerchantId = createPaymentRequest.MerchantId,
                Amount = createPaymentRequest.Amount,
                Currency = createPaymentRequest.Currency
            };

            await _context.AddAsync<Payment>(payment);

            await _context.SaveChangesAsync();
        }
    }
}