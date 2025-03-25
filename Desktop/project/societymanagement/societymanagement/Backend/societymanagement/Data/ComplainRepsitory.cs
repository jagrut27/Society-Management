using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;
using societymanagement.Entity;

namespace societymanagement.Data
{
  public class ComplainRepsitory
  {
    private readonly SqlConnection _connection;


    public ComplainRepsitory()
    {
      try
      {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement_new;Integrated Security=True;TrustServerCertificate=True;";
        _connection = new SqlConnection(connectionString);
        _connection.Open();  // Test connection
        Console.WriteLine("Database connection successful!");
        _connection.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Database connection failed: {ex.Message}");
      }
    }

    public String AddComplain(Complain complain)
    {
      try
      {
        _connection.Open();

        using (SqlCommand cmd = new SqlCommand("sp_InsertComplain", _connection))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Memberid",complain.MemberId);
          cmd.Parameters.AddWithValue("@Title", complain.Title);
          cmd.Parameters.AddWithValue("@Description", complain.Description);
      

          cmd.ExecuteNonQuery();

          return "complain Successfully!!!!!";
        }




      }
      catch (Exception e)
      {
        throw new Exception("Error Adding New complain", e);
      }
    }

    public List<Complain> GetComplainByid(int memberid)
    {

      List<Complain> complains = new List<Complain>();
      try
      {
        _connection.Open();

        using (SqlCommand cmd = new SqlCommand("GetMemberWithComplaintbyid", _connection))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Memberid", memberid);

          SqlDataAdapter dataadpter = new SqlDataAdapter(cmd);
          DataTable dataTable = new DataTable();
          dataadpter.Fill(dataTable);



  

          foreach(DataRow row in dataTable.Rows)
          {
            Complain complaindata = new Complain
            {
              MemberId = Convert.ToInt32(row["member_id"]),
              Title = row["title"].ToString(),
              Description = row["description"].ToString(),
              Status = row["status"].ToString(),
              CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : (DateTime?)null
            };

            complains.Add(complaindata);
          }
        };
      }
      catch(Exception e)
      {
        throw new Exception("Error Fetching Complain By Id", e);
      }
      finally
      {
        _connection.Close();
      }

      return complains;
    }



  }
}
