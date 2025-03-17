using System.Security.Policy;
using System.Threading.Tasks;
using Admin.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MembersController : ControllerBase
  {
    private readonly ApplicationdbContext _context;

    public MembersController(ApplicationdbContext context)
    {
      _context = context;
    }
    [HttpGet]
  //  [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllMembers()
    {
      return Ok(await _context.Members.ToListAsync());
    }
    [HttpDelete("{id}")]
  //  [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMemberAsync(int id)
    {
      var MemberToDelete= await _context.Members.FindAsync(id);
      if (MemberToDelete == null) return NotFound();
      _context.Members.Remove(MemberToDelete);
      await _context.SaveChangesAsync();
      return Ok();
    }
      

  }

}
