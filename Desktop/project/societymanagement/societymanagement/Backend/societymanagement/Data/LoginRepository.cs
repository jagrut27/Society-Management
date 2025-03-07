using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using societymanagement.Entity;

namespace societymanagement.Data
{
    public class LoginRepository
    {
        private readonly SqlConnection _connection;
        //private readonly char[] _jwtSecretKey;

        public LoginRepository()
        {
            try
            {
                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement;Integrated Security=True;TrustServerCertificate=True;";
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

                        //DataRow row = data.Rows[0];
                        //return new Login
                        //{
                        //    Id = Convert.ToInt32(row["id"])

                        //};
                        // ✅ Retrieve the stored hashed password from the database
                        string storedHashedPassword = data.Rows[0]["Password"].ToString();
                        int MemberId = Convert.ToInt32(data.Rows[0]["Memberid"]);

                        // ✅ Compare the entered password (plain text) with the stored hashed password
                        //bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);

                        if (password == storedHashedPassword)
                        {
                            //string token = GenerateJwtToken(email);
                            //return token; // Return the JWT token instead of plain message
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

        //private string GenerateJwtToken(string email)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.Email, email),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token ID
        //};

        //    var token = new JwtSecurityToken(
        //        issuer: "YourAppName",
        //        audience: "YourAppUsers",
        //        claims: claims,
        //        expires: DateTime.Now.AddHours(1), // Token expires in 1 hour
        //        signingCredentials: credentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
