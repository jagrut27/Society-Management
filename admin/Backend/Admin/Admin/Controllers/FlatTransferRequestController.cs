using System.Data;
using System.Data.SqlClient;
using Admin.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FlatTransferRequestController : ControllerBase
  {
    private readonly ApplicationdbContext _context;
    public FlatTransferRequestController(ApplicationdbContext context)
    {
      _context = context;
    }
    [HttpGet]
    //  [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRequset()
    {
      return Ok(await _context.FlatTransferRequest.ToListAsync());
    }
    
    [HttpPut("UpdateStatus/{id}")]
    public IActionResult UpdateStatus(int id)
    {
      var flatTransfer = _context.FlatTransferRequest.FirstOrDefault(ft => ft.Transfer_id == id);
      if (flatTransfer == null)
        return NotFound("Flat Transfer not found");

      flatTransfer.status = flatTransfer.status == "Pending" ? "Approved" : "Pending";
      _context.SaveChanges();

      return Ok("Status updated successfully");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFlatTransfer(int id)
    {
      var flat = _context.FlatTransferRequest.FirstOrDefault(f => f.Transfer_id == id);
      if (flat == null)
        return NotFound("Record not found");

      _context.FlatTransferRequest.Remove(flat);
      _context.SaveChanges();

      return Ok("Flat Transfer deleted successfully");
    }


  }
}
