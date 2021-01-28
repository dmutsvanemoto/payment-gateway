using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentsGateway.Models;

namespace PaymentsGateway.Services
{
    public interface IPaymentsService
    {
        Task<bool> ProcessPayment(PaymentRequest payload);

        Task<IList<PaymentsContract>> RetrievePayments(int merchantId);
    }
}
