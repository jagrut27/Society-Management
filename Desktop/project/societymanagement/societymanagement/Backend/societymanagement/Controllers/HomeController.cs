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

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        id=member.MemberId,
                        Firstname = member.Firstname,
                        Lastname = member.Lastname,
                        PhoneNumber = member.PhoneNumber,
                       
                        FlatNumber = member.FlatNumber,
                        BlockNumber = member.BlockNumber
                    }
                });
            }
            catch(Exception e)
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
                    return BadRequest(new { success = false, message = result }); // 400 Bad Request
                }
                else if (result.Contains("Error: Flat Number Registered In This Block...."))
                {
                    return BadRequest(new { success = false, message = result }); // 400 Bad Request
                }
                else if (result.Contains("Error"))
                {
                    return StatusCode(500, new { success = false, message = result }); // 500 Internal Server Error
                }

                return Ok(new { success = true, message = result }); // 200 OK
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
            }
        }

    }
}
