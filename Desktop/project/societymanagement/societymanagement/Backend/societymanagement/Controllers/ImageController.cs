//using System.Data.SqlClient;
//using System.Data;
//using Microsoft.AspNetCore.Mvc;
//using societymanagement.Data;
//using societymanagement.Entity;

//namespace societymanagement.Controllers
//{
//  [Route("api/image")]
//  [ApiController]
//  public class ImageController : Controller
//  {
//    private readonly SqlConnection _connection;

//    public ImageController()
//    {
//      try
//      {
//        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement;Integrated Security=True;TrustServerCertificate=True;";
//        _connection = new SqlConnection(connectionString);
//        _connection.Open();  // Test connection
//        Console.WriteLine("Database connection successful!");
//        _connection.Close();
//      }
//      catch (Exception ex)
//      {
//        Console.WriteLine($"Database connection failed: {ex.Message}");
//      }
//    }

//    [HttpPut]
//    public async Task<ActionResult<Members>> AddDataWithImageName([FromForm] Members product)
//    {

//      if (product.ImageFile != null)
//      {
//        var path = Path.Combine(Directory.GetCurrentDirectory(), "imageupload");
//        if (!Directory.Exists(path))
//        {
//          Directory.CreateDirectory(path);
//        }

//        var filePath = Path.Combine(path, $"{Guid.NewGuid()}_{Path.GetFileName(product.ImageFile.FileName)}");
//        using (var fs = new FileStream(filePath, FileMode.Create))
//        {
//          await product.ImageFile.CopyToAsync(fs);
//        }
//        product.ImageUrl = Path.GetFileName(filePath);
//      }

//      try { 
//      //string query = "Insert into Product (Name,Address) Values (@name,@address)";
//      using (SqlCommand command = new SqlCommand("sp_UpdateProfile", _connection))
//      {

//        command.CommandType = CommandType.StoredProcedure;

//        command.Parameters.AddWithValue("@MemberId", product.MemberId);
//        command.Parameters.AddWithValue("@imageurl", product.ImageUrl);
//        command.Parameters.AddWithValue("@Firstname", product.Firstname);
//        command.Parameters.AddWithValue("@Lastname", product.Lastname);
//        command.Parameters.AddWithValue("@Email", product.Email);
//        command.Parameters.AddWithValue("@Password", product.Password);
//        command.Parameters.AddWithValue("@PhoneNumber", product.PhoneNumber);

//        command.Parameters.AddWithValue("@FlatNumber", product.FlatNumber);
//        command.Parameters.AddWithValue("@BlockNumber", product.BlockNumber);
//        _connection.Open();
//        int rows = await command.ExecuteNonQueryAsync();
//        _connection.Close();
//        if (rows > 0)
//        {
//          return Ok(new { message = "Record updated successfully", data = product });
//        }
//        else
//        {
//          return BadRequest(new { message = "record are not updated" });
//        }
//      }
//    }
//            catch (Exception ex)
//            {
//        return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
//      }



//    }


//    [HttpGet]
//    public ActionResult<List<Image>> GetAllWithImage()
//    {
//      List<Image> products = new List<Image>();
   
//        //string query = "select * from Product";
//        using (SqlCommand cmd = new SqlCommand("sp_imageget", _connection))
//        {

//          cmd.CommandType = CommandType.StoredProcedure;
//        _connection.Open();

//          SqlDataReader reader = cmd.ExecuteReader();
//        while (reader.Read())
//        {
//          Image product = new Image
//          {
//            MemberId = Convert.ToInt32(reader["MemberId"]),
//            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageURL")) ? "" : reader["ImageURL"].ToString()
//          };
//          products.Add(product);
//        }
//        var demo = products.Select(item => new
//          {
//            item.MemberId,
//            //item.Name,
//            ImageURL = $"{Request.Scheme}:/{Request.Host}/imageupload/{item.ImageUrl}",
//          });
//        _connection.Close();
//          return Ok(demo);

        

//      }
//    }
 

//  }
//}
