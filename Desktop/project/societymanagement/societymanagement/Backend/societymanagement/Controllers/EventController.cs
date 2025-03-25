using Microsoft.AspNetCore.Mvc;
using societymanagement.Data;
using societymanagement.Entity;

namespace societymanagement.Controllers
{
  [Route("api/Event")]
  [ApiController]
  public class EventController : Controller
  {
      private readonly EventRepository eventRepository;

    public EventController(EventRepository eventRepository)
    {
      this.eventRepository = eventRepository;
    }

    [HttpGet]
    public ActionResult<List<Complain>> GetComplainAll()
    {
      try
      {
        var complaindata = eventRepository.GetComplain();

        if (complaindata == null) {
          return Ok("complain not found");
        }
        else
        {
          //return Ok(new { success = true, message = result });

          return Ok( complaindata);
        }

      }
      catch (Exception ex) {

        return StatusCode(500,$"Internal Server Error:{ex.Message}");
      }
    
    }
  }
}
