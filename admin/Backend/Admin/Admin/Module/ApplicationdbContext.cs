using Microsoft.EntityFrameworkCore;

namespace Admin.Module
{
  public class ApplicationdbContext :DbContext 
  {
    public ApplicationdbContext (DbContextOptions<ApplicationdbContext> options):base(options) { }
    public DbSet<Events>Events { get; set; }
  }

}
