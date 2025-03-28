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
  }
}
