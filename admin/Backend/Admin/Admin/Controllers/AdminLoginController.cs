using System.Data.SqlClient;
using Admin.Module;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
  {
    private readonly IConfiguration _configuration;

    public AdminLoginController(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody]AdminLogin login )
    {
      string connectionString = _configuration.GetConnectionString("DefaultConnection");

      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        conn.Open();
        string query = "select password from  Admin where Email = @Email  ";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          cmd.Parameters.AddWithValue("@Email", login.Email);
         

          object result = cmd.ExecuteScalar();
          if(result !=null)
          {
            string Storedpassword = result.ToString();
            if (login.password == Storedpassword)
            {
              return Ok(new { Message = "Login Succesful", status = false });
            }
          }
        }

      }
      return  Unauthorized(new { Message ="Invalid Email or Password ", status =false});
    }

    }

  
}
public class AdminLoginRequest
{
  public string Email { get; set; }
  public string Password { get; set; }
}
