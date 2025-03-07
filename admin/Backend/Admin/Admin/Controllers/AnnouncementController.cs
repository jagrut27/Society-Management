using Admin.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
    private readonly ApplicationdbContext  _context;
    private readonly EmailService _emailService;

    public AnnouncementController(ApplicationdbContext contenxt , EmailService emailService)
    {
      _context = contenxt;
      _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult>PostAnnouncement([FromBody] Announcement announcement)
    {
      _context.Announcement.Add(announcement);
      await _context.SaveChangesAsync();
      var members = _context.Users.Where(u => u.role == "Member").ToList();
      foreach (var member in members) 
      {
        await _emailService.SendEmailAsync(member.email, "New Announcement", announcement.Description);
      }

      return Ok(new { message = "Announcement added and emails sent." });
    }
    [HttpGet]
    public IActionResult GetAnnouncements()
    {
      return Ok(_context.Announcement.ToList());
    }

  }
}
