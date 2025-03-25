using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using Admin.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {

    private readonly IConfiguration _configuration;

    public ComplaintsController(IConfiguration  configuration)
    {
      _configuration = configuration;
    }
    [HttpGet]
    public async Task<IActionResult> GetComplaints()  
    {
      List<Complaints> complaints = new List<Complaints>();
      using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("GetMemberWithComplaint", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
          complaints.Add(new Complaints
          {
            complaints_id=Convert.ToInt32(reader["complaints_id"]),
            member_id = Convert.ToInt32(reader["member_id"]),
            Firstname = reader["Firstname"].ToString(),
            Lastname = reader["Lastname"].ToString(),
            FlatNumber = reader["FlatNumber"].ToString(),
            BlockNumber = reader["BlockNumber"].ToString(),
            Title = reader["Title"].ToString(),
            Description = reader["Description"].ToString(),
            Status = reader["Status"].ToString(),
            created_at = Convert.ToDateTime(reader["created_at"])
          });


        }
      }
      return Ok(complaints);

    }
    [HttpGet("recent")]
    public IActionResult GetRecentComplaints()
    {
      using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
      {
        conn.Open();
        string query = @"
            SELECT c.complaints_id, c.member_id, 
                   m.Firstname, m.Lastname, m.FlatNumber, m.BlockNumber, 
                   c.Title, c.Description, c.Status, c.created_at
            FROM Complaints c
            JOIN Members m ON c.member_id = m.MemberId
            WHERE DATEDIFF(DAY, c.created_at, GETDATE()) <= 7
            ORDER BY c.created_at DESC";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          SqlDataReader reader = cmd.ExecuteReader();
          List<Complaints> complaints = new List<Complaints>(); 

          while (reader.Read())
          {
            complaints.Add(new Complaints
            {
              complaints_id = Convert.ToInt32(reader["complaints_id"]),
              member_id = Convert.ToInt32(reader["member_id"]),
              Firstname = reader["Firstname"].ToString(),
              Lastname = reader["Lastname"].ToString(),
              FlatNumber = reader["FlatNumber"].ToString(),
              BlockNumber = reader["BlockNumber"].ToString(),
              Title = reader["Title"].ToString(),
              Description = reader["Description"].ToString(),
              Status = reader["Status"].ToString(),
              created_at = Convert.ToDateTime(reader["created_at"])
            });
          }
          return Ok(complaints);
        }
      }
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteComplaint(int id)
    {
      using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("DELETE FROM Complaints WHERE complaints_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);

       int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
          return Ok(new { message = "Complaint deleted successfully." });
        else
          return NotFound(new { message = "Complaint not found." });
      }
    }
    [HttpPut("UpdateComplaintStatus")]
    public IActionResult UpdateComplaintStatus([FromBody] UpdateComplaint model)
    {
      using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
      {
        conn.Open();
        using (SqlCommand cmd = new SqlCommand("UPDATE Complaints SET Status = @status WHERE complaints_id = @id", conn))
        {
          cmd.Parameters.AddWithValue("@status", model.Status);
          cmd.Parameters.AddWithValue("@id", model.complaints_id);

          int rowsAffected = cmd.ExecuteNonQuery();
          if (rowsAffected > 0)
          {
            return Ok(new { message = "Complaint status updated successfully.", status = model.Status });
          }
          else
          {
            return NotFound(new { message = "Complaint not found." });
          }
        }
      }
    }
    [HttpGet("count")]
    public async Task<IActionResult> GetTotalComplaints()
    {
      int totalComplaints = 0;

      using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
      {
        await conn.OpenAsync();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Complaints", conn);

        totalComplaints = Convert.ToInt32(await cmd.ExecuteScalarAsync());
      }

      return Ok(new { totalComplaints });
    }


  }
}
public class UpdateComplaint
{
  public int complaints_id { get; set; }
  public string Status { get; set; }
}
