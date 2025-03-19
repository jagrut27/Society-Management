using System.Data.SqlClient;
using System.Data;

namespace societymanagement.Data
{
  public class LoginRepository
  {

    private readonly SqlConnection _connection;
   

    public LoginRepository()
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

    public string AuthenticateUser(string email, string password)
    {
      try
      {
        _connection.Open(); // Open database connection

        using (SqlCommand cmd = new SqlCommand("sp_checkemail", _connection))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Email", email);

          SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          DataTable data = new DataTable();
          adapter.Fill(data);

          if (data.Rows.Count > 0)
          {


            // ✅ Retrieve the stored hashed password from the database
            string storedHashedPassword = data.Rows[0]["Password"].ToString();
            int MemberId = Convert.ToInt32(data.Rows[0]["Memberid"]);

            // ✅ Compare the entered password (plain text) with the stored hashed password
            //bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);

            if (password == storedHashedPassword)
            {
             
              return MemberId.ToString();  // Return the userId as string on success

            }
            else
            {
              return "Invalid Password";
            }
          }
          else
          {
            return "User not found";
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error during authentication", ex);
      }
      finally
      {
        _connection.Close(); // Ensure the connection is closed
      }
    }


  }
}
