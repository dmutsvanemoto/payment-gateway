using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Data;
using PaymentsAPI.Data.Models;

namespace PaymentsAPI.Business.Services
{
    public class ReadService : IReadService
    {
        private readonly PaymentsDbContext _context;

        public ReadService(PaymentsDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Payment>> GetByMerchantId(int merchantId)
        {
            var payments = await _context.Payments
                .Where(x => x.MerchantId == merchantId)
                .ToListAsync();

            return payments;
        }
    }
}