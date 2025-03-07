using System.Data;
using System.Data.SqlClient;
using societymanagement.Entity;

namespace societymanagement.Data
{
    public class ImageRepository
    {
        private readonly SqlConnection _connection;

        public ImageRepository()
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

        //public String Addimage(Image image)
        //{
        //    try
        //    {
        //        _connection.Open();

        //        using(SqlCommand cmd=new SqlCommand("sp_imageupload", _connection))
        //        {
        //            cmd.CommandType=CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@MemberId", image.Id);
        //            cmd.Parameters.AddWithValue("@imageurl", image.ImageUrl);


        //            int rowsAffected = cmd.ExecuteNonQuery();

        //            return rowsAffected > 0 ? "Image Uploaded Successfully" : "Image Upload Failed";


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error Uploading Image", ex);
        //    }
        //}

    }
}
