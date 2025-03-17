using Microsoft.AspNetCore.Mvc;
using societymanagement.Data;
using societymanagement.Entity;

namespace societymanagement.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginRepository _repository;

        public LoginController(LoginRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("/authenticateuser")]
        public IActionResult Login([FromBody] Login login)
        {
          

            try
            {
                string result = _repository.AuthenticateUser(login.Email, login.Password);

         

                if (result == "Invalid Password")
                {
                    return Ok(new { success = false, message = "Invalid Password" });
                }
                else if (result == "User not found")
                {
                    return Ok(new { success = false, message = "User not found" });
                }
                else
                {
                    // Successful login, return userId
                    return Ok(new
                    {
                        success = true,
                        message = "Login Successful",

                        data = new
                        {
                            MemberId = result // Assuming 'result' contains the user ID
                        }

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
