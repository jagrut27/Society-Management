using Microsoft.EntityFrameworkCore;

namespace Admin.Module
{
  public class ApplicationdbContext :DbContext 
  {
    public ApplicationdbContext (DbContextOptions<ApplicationdbContext> options):base(options) { }
    public DbSet<Events>Events { get; set; }
    public DbSet<Announcement> Announcement { get; set; }
    public DbSet <Users> Users { get; set; }

    public DbSet<Members>Members { get; set; }

    public DbSet<MaintenancePayment> MaintenancePayment { get; set; }
    public DbSet<FlatTransferRequest> FlatTransferRequest { get; set; }

  //  public DbSet<Complaints> Complaints { get; set; }

    
  }

} 
