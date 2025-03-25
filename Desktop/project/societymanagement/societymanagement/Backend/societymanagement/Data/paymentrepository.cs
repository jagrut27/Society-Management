using System;
using System.Data;
using System.Data.SqlClient;
using societymanagement.Entity;

namespace societymanagement.Data
{
  public class PaymentRepository
  {
    private readonly string _connectionString;

    public PaymentRepository()
    {
      _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement_new;Integrated Security=True;TrustServerCertificate=True;";
    }

    public string AddPayment(PaymentRequest payment)
    {
      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        try
        {
          conn.Open();
          using (SqlCommand cmd = new SqlCommand("sp_insertpayment", conn))
          {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memberid", payment.MemberId);
            cmd.Parameters.AddWithValue("@name", payment.Name);
            cmd.Parameters.AddWithValue("@email", payment.email);
            cmd.Parameters.AddWithValue("@flatnumber", payment.FlatNumber);
            cmd.Parameters.AddWithValue("@blocknumber", payment.BlockNumber);
            cmd.Parameters.AddWithValue("@Amout", payment.Amount); // Keeping as 'Amout' since SP uses this
            cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
            cmd.Parameters.AddWithValue("@status", payment.Status);

            cmd.ExecuteNonQuery();
            return "Payment Successfully Added!";
          }
        }
        catch (SqlException sqlEx)
        {
          Console.WriteLine($"SQL Error: {sqlEx.Message}");
          return $"SQL Error: {sqlEx.Message}";
        }
        catch (Exception e)
        {
          Console.WriteLine($"Unexpected Error: {e.Message}");
          return $"Unexpected Error: {e.Message}";
        }
      }
    }
    public List<PaymentHistory> GetPaymentHistory(int memberId)
    {
      List<PaymentHistory> payments = new List<PaymentHistory>();

      using (var conn = new SqlConnection(_connectionString))
      {
        conn.Open();
        string query = @"
            SELECT mp.PaymentId, mp.Amount, mp.Status, mp.PaymentDate, 
                   m.FirstName + ' ' + m.LastName AS Fullname, 
                   m.FlatNumber, m.BlockNumber
            FROM MaintenancePayment mp
            JOIN Members m ON mp.MemberId = m.MemberId
            WHERE mp.MemberId = @MemberId
            ORDER BY mp.PaymentDate DESC";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          cmd.Parameters.AddWithValue("@MemberId", memberId);
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            payments.Add(new PaymentHistory
            {
              PaymentId = Convert.ToInt32(reader["PaymentId"]),
              Name = reader["Fullname"].ToString(),
              PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
              FlatNumber = reader["FlatNumber"].ToString(),
              BlockNumber = reader["BlockNumber"].ToString(),
              Amount = Convert.ToDecimal(reader["Amount"]),
            });
          }
        }
      }
      return payments;
    }

  }
}
