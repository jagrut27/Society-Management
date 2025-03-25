using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using societymanagement.Data;
using societymanagement.Entity;

namespace societymanagement.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PaymentController : ControllerBase
  {

    private readonly PaymentRepository paymentrepository;

    public PaymentController(PaymentRepository paymentrepository)
    {
      this.paymentrepository = paymentrepository;
    }

    [HttpPost("create-order")]
    public IActionResult CreateOrder([FromBody] PaymentRequest Addpayment)
    {
      try
      {
        string result = paymentrepository.AddPayment(Addpayment);


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




    [HttpGet("payment-history/{memberId}")]
    public IActionResult GetPaymentHistory(int memberId)
    {
      try
      {
        var payments = paymentrepository.GetPaymentHistory(memberId);

        if (payments == null || payments.Count == 0)
        {
          return NotFound(new { success = false, message = "No payments found for this member." });
        }

        return Ok(payments);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
      }
    }

  

  /*  [HttpGet("all-payments")]
    public async Task<IActionResult> GetAllPayments()
    {
      List<PaymentHistory> payments = new List<PaymentHistory>();

      using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
      {
        conn.Open();
        string query = @"SELECT p.PaymentId, p.Amount, p.PaymentDate, 
                                m.Firstname, m.Lastname, m.FlatNumber, m.BlockNumber 
                         FROM MaintenancePayments p
                         JOIN Members m ON p.MemberId = m.MemberId
                         ORDER BY p.PaymentDate DESC";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            payments.Add(new PaymentHistory
            {
              PaymentId = Convert.ToInt32(reader["PaymentId"]),
              Amount = Convert.ToDecimal(reader["Amount"]),
              PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
              Name = $"{reader["Firstname"]} {reader["Lastname"]}".ToString(),
              FlatNumber = reader["FlatNumber"].ToString(),
              BlockNumber = reader["BlockNumber"].ToString()
            });
          }
        }
      }
      return Ok(payments);
    }

    */

}
}


  

