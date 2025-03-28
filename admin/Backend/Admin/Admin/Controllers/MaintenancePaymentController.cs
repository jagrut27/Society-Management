using System.Data.SqlClient;
using Admin.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MaintenancePaymentController : ControllerBase
  {
    private readonly ApplicationdbContext _context;

    public MaintenancePaymentController(ApplicationdbContext context)
    {
      _context = context;
    }

    [HttpGet("all-maintenance-payments")]
    public async Task<IActionResult> GetAllMaintenancePayments()
    {
      return Ok(await _context.MaintenancePayment.ToListAsync());

    } 


    //  return Ok(payments);

    [HttpGet("last3days")]
    public IActionResult GetRecentPayments()
    {
      DateTime threeDaysAgo = DateTime.Now.AddDays(-3);

      var recentPayments = _context.MaintenancePayment
          .Where(p => p.PaymentDate >= threeDaysAgo)
          .OrderByDescending(p => p.PaymentDate)
          .ToList();

      return Ok(recentPayments);
    }



  }
}
