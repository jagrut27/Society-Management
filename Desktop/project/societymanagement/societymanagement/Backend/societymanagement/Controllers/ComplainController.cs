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

    [HttpGet("complainby-id")]
    public ActionResult<List<Complain>> GetComplainbyId(int memberid)
    {
      try
      {
        var ComplainData = complainRepsitory.GetComplainByid(memberid);

        if(ComplainData == null)
            return Ok(new{ success=false,message="user id not found"});

        return Ok(new
        {
          success = true,
          data = ComplainData.Select(c => new
          {
            member_id = c.MemberId,
            title = c.Title,
            description = c.Description,
            status = c.Status,
            created_at = c.CreatedAt
          })
        });

      }
      catch(Exception e)
      {
        return StatusCode(500, new { success = false, message = $"Unexcepted Error:{e.Message}" });
      }
    }
  }
}
