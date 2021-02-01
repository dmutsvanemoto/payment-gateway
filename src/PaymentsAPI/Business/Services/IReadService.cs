using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentsAPI.Data.Models;

namespace PaymentsAPI.Business.Services
{
    public interface IReadService
    {
        Task<IList<Payment>> GetByMerchantId(int merchantId);
    }
}
