using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace societymanagement.Entity
{
  public class ApplicationdbContext :DbContext
  {
    public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options) : base(options) { }

    public DbSet<PaymentRequest> PaymentRequest { get; set; }

  }
}
