using Microsoft.AspNetCore.Mvc;
using societymanagement.Data;
using societymanagement.Entity;

namespace societymanagement.Controllers
{
  [Route("api/complain")]
  [ApiController]
  public class ComplainController : Controller
  {

    private readonly ComplainRepsitory complainRepsitory;

    public ComplainController(ComplainRepsitory complainRepsitory)
    {
      this.complainRepsitory = complainRepsitory;
    }

    [HttpPost("addcomplain")]
    public ActionResult AddComplain([FromBody]Complain complain)  
    {
      try
      {
        string result = complainRepsitory.AddComplain(complain);

    
        if (result.Contains("Error"))
        {
          return StatusCode(500, new { success = false, message = result }); // 500 Internal Server Error
        }
        else
        {
          //return Ok(new { success = true, message = result });

          return Ok(new
          {
            success = true,
            message = result

             



          });
        }


      }

      catch (Exception ex)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
      }
    }
  }
}
