using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using societymanagement.Data;
using societymanagement.Entity;

namespace societymanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : Controller
    {
    private readonly AnnouncementRepo announcement;

    public AnnouncementController(AnnouncementRepo announcement)
    {
      this.announcement = announcement;
    }
    [HttpGet]
    [Route("GetAll")]
    public IActionResult GetAllAnnouncements()
    {
      try
      {
        List<Announcement> announcements = announcement.GetAnnouncement();
        return Ok(new { success = true, data = announcements });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
      }
    }
  }
}
