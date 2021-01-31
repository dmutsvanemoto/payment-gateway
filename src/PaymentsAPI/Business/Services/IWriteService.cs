using System.Threading.Tasks;
using PaymentsAPI.Data.Models.Requests;

namespace PaymentsAPI.Business.Services
{
    public interface IWriteService
    {
        Task CreatePayment(CreatePaymentRequest createPaymentRequest);
    }
}