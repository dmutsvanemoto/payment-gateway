using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Data.Models;

namespace PaymentsAPI.Data
{
    public class PaymentsDbContext : DbContext
    {
        public PaymentsDbContext()
        {
            
        }

        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Payment> Payments { get; set; }
    }
}
