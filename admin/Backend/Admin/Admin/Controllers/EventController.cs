using Admin.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Admin.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventController : ControllerBase
  {
    private readonly ApplicationdbContext _context;

    public EventController(ApplicationdbContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
      return Ok(await _context.Events.ToListAsync());
    }
    [HttpPost]
    public async Task<IActionResult> CreateEventAsync([FromBody] Events eventData)
    {
      if (eventData == null)
      {
        return BadRequest("Event data is required.");
      }
     
      await _context.Events.AddAsync(eventData);  
      await _context.SaveChangesAsync();

      return Ok(eventData);
    }
    [HttpPut("{event_id}")]
    public async Task<IActionResult>UpdateEvent (int event_id, [FromBody] Events UpdateEvent)
    {
        var existingEvent = await _context.Events.FindAsync(event_id);
      if (existingEvent == null) return NotFound();
      existingEvent.event_name = UpdateEvent.event_name;
      existingEvent.Description = UpdateEvent.Description;
      existingEvent.place = UpdateEvent.place;
      existingEvent.event_date = UpdateEvent.event_date;
      existingEvent.event_time = UpdateEvent.event_time;
        
      await _context.SaveChangesAsync();
      return Ok(existingEvent);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult>DeleteEvent (int id)
    {
      var eventTodelete = await _context.Events.FindAsync(id);
      if (eventTodelete == null) return NotFound();
      _context.Events.Remove(eventTodelete);
      await _context.SaveChangesAsync();
      return Ok();
    }
  }
}
