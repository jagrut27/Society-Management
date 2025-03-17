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
      var members = _context.Members.Where(u => u.role == "Member").ToList();
      foreach (var member in members) 
      {
        await _emailService.SendEmailAsync(member.Email, "New Announcement", announcement.Description);
      }

      return Ok(new { message = "Announcement added and emails sent." });
    }
    [HttpGet]
    public IActionResult GetAnnouncements()
    {
      return Ok(_context.Announcement.ToList());
    }

    [HttpPut("{announcement_id}")]
    public async Task<IActionResult> UpdateAnnouncement(int announcement_id, [FromBody] Announcement UpdateAnnouncement)
    {
      var existingAnnouncement = await _context.Announcement.FindAsync(announcement_id);
      if (existingAnnouncement == null) return NotFound();
      existingAnnouncement.Announcement_name = UpdateAnnouncement.Announcement_name;
      existingAnnouncement.Description = UpdateAnnouncement.Description;
      existingAnnouncement.date = UpdateAnnouncement.date;
   /*   existingAnnouncement.event_date = UpdateAnnouncement.event_date;
      existingAnnouncement.event_time = UpdateAnnouncement.event_time;*/

      await _context.SaveChangesAsync();
      return Ok(existingAnnouncement);
    }

    [HttpDelete("{announcement_id}")]

    public async Task<IActionResult> DeleteAnnouncement(int announcement_id)
    {
      var AnnouncementTodelete = await _context.Announcement.FindAsync(announcement_id);
      if (AnnouncementTodelete == null) return NotFound();
      _context.Announcement.Remove(AnnouncementTodelete);
      await _context.SaveChangesAsync();
      return Ok();
    }

  }
}
