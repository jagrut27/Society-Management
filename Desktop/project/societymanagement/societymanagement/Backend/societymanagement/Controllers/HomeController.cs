using Microsoft.AspNetCore.Mvc;
using societymanagement.Data;
using societymanagement.Entity;
using System;
using System.Collections.Generic;


namespace societymanagement.Controllers
{
  [Route("api/home")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    private readonly MemberRepository memberRepository;

    public HomeController(MemberRepository memberRepository)
    {
      this.memberRepository = memberRepository;
    }


    // GET: api/home/members
    [HttpGet("members")]
    public ActionResult<List<Members>> GetAllMembersList()
    {
      try
      {
        var member = memberRepository.GetAllMembers();

        if (member == null || member.Count == 0)
        {
          return NotFound("No members found.");
        }

        return Ok(member);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
      }
    }

    [HttpGet("by-email")]
    public IActionResult GetMemberByEmail(string email)
    {
      try
      {
        var member = memberRepository.GetMemberByEmail(email);

        if (member == null)
          return NotFound(new { success = false, message = "User not found" });


        //var imageUrl = string.IsNullOrEmpty(member.ImageUrl)
        //    ? null
        //    : $"{Request.Scheme}://{Request.Host}/imageupload/{member.ImageUrl}";


        return Ok(new
        {
          success = true,
          data = new
          {
            id = member.MemberId,
            Firstname = member.Firstname,
            Lastname = member.Lastname,
            PhoneNumber = member.PhoneNumber,

            FlatNumber = member.FlatNumber,
            BlockNumber = member.BlockNumber,
         
            //ImageURL = imageUrl
          }
        });
      }
      catch (Exception e)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {e.Message}" });
      }

    }


    [HttpPost("addmembers")]
    public ActionResult AddMembers([FromBody] Members member)
    {
      try
      {
        string result = memberRepository.AddMember(member);

        if (result.Contains("Error: Email is already registered"))
        {
          return Ok(new { success = false, message = "Email is already registred" }); // 400 Bad Request
        }
        else if (result.Contains("Error: Flat Number Registered In This Block...."))
        {
          return Ok(new { success = false, message = "Flat Number Registered In This Block...." }); // 400 Bad Request
        }
        else if (result.Contains("Error"))
        {
          return StatusCode(500, new { success = false, message = result }); // 500 Internal Server Error
        }
        else
        {
          return Ok(new { success = true, message = result }); 
        }


      }

      catch (Exception ex)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
      }
    }

    [HttpDelete("DeleteProfile")]

    public ActionResult DeleteMember( int memberid)
    {
      try
      {
        String result = memberRepository.deletemember(memberid);

        if (result.Contains("Error"))
        {
          return Ok(new { success = false, message = "Member Id Not Found...." }); // 400 Bad Request

        }
        return Ok(new { success = true, message = result });
        
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
      }


    }
      [HttpPut("updateprofile")]
      public async Task<ActionResult> UpdateMember([FromQuery]int MemberId, [FromForm] EditMember member)  // ✅ Change [FromBody] to [FromForm]
    {

     
      try
        {

       

        Console.WriteLine($"Received MemberId: {MemberId}");
        Console.WriteLine($"Received Firstname: {member.Firstname}");
        Console.WriteLine($"Received Lastname: {member.Lastname}");
        //Console.WriteLine($"Received Email: {member.Email}");
        //Console.WriteLine($"Received Password: {member.Password}");
        Console.WriteLine($"Received PhoneNumber: {member.PhoneNumber}");
        Console.WriteLine($"Received FlatNumber: {member.FlatNumber}");
        Console.WriteLine($"Received BlockNumber: {member.BlockNumber}");

        if (MemberId <= 0)
        {
          return BadRequest(new { success = false, message = "Invalid MemberId" });
        }

        member.MemberId = MemberId;
        string result = await memberRepository.updatememberAsync(member);

          if (result.Contains("No records were updated"))
          {
            return Ok(new { success = false, message = result });
          }

          return Ok(new { success = true, message = result });
        }
        catch (Exception ex)
        {
          return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
        }
      }


    [HttpGet("by-id")]
    public IActionResult FetchMemberById([FromHeader]int memberid)
    {
      try
      {
        var member = memberRepository.FetchAfterUpdate(memberid);

        if (member == null)
          return NotFound(new { success = false, message = "User not found" });


        var imageUrl = string.IsNullOrEmpty(member.ImageUrl)
            ? null
            : $"{Request.Scheme}://{Request.Host}/imageupload/{member.ImageUrl}";


        return Ok(new
        {
          success = true,
          data = new
          {
            id = member.MemberId,
            Firstname = member.Firstname,
            Lastname = member.Lastname,
            PhoneNumber = member.PhoneNumber,

            FlatNumber = member.FlatNumber,
            BlockNumber = member.BlockNumber,

            ImageURL = member.ImageUrl
          }
        });
      }
      catch (Exception e)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {e.Message}" });
      }

    }


  }
}

