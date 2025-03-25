using System.Data;
using System.Data.SqlClient;
using societymanagement.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace societymanagement.Data
{
  public class FlatTransferRepository
  {

    private readonly SqlConnection _connection;

    public FlatTransferRepository()
    {
      try
      {
        string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement_new;Integrated Security=True;TrustServerCertificate=True;";
        _connection = new SqlConnection(connectionstring);
        _connection.Open();
        Console.WriteLine("Database Connection Sucessfull!");
        _connection.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine($"databse Connection Failed:{e.Message}");
      }
    }



    public async Task<bool> InsertFlatTransfer(FlatTransfer flattransfer)
    {
      try
      {

        _connection.Open();

        if (flattransfer.ImageIdentityproofFile != null)
        {
          var path = Path.Combine(Directory.GetCurrentDirectory(), "imageupload");

          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

          var filePath = Path.Combine(path, $"{Guid.NewGuid()}_{Path.GetFileName(flattransfer.ImageIdentityproofFile.FileName)}");
          using (var fs = new FileStream(filePath, FileMode.Create))
          {
            await flattransfer.ImageIdentityproofFile.CopyToAsync(fs);  // Fixed
          }
          flattransfer.Identityproofurl = filePath;
        }

        if (flattransfer.Agreementcopyfile != null)
        {
          var path = Path.Combine(Directory.GetCurrentDirectory(), "imageupload");

          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

          var filePath = Path.Combine(path, $"{Guid.NewGuid()}_{Path.GetFileName(flattransfer.Agreementcopyfile.FileName)}");
          using (var fs = new FileStream(filePath, FileMode.Create))
          {
            await flattransfer.Agreementcopyfile.CopyToAsync(fs);  // Fixed
          }
          flattransfer.Agreementcopyurl = filePath;
        }



        using (SqlCommand cmd = new SqlCommand("sp_InsertFlatTransferRequest", _connection))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Currentownername", flattransfer.CurrentOwnerName);
          cmd.Parameters.AddWithValue("@Currentownercontactnumber", flattransfer.CurrentOwnerContactNumber);
          cmd.Parameters.AddWithValue("@flatnumber", flattransfer.FlatNumber);
          cmd.Parameters.AddWithValue("@blocknumber", flattransfer.BlockNumber);
          cmd.Parameters.AddWithValue("@Newownerfullname", flattransfer.NewOwnerFullName);
          cmd.Parameters.AddWithValue("@Newownercontactnumber", flattransfer.NewOwnerContactNumber);
          cmd.Parameters.AddWithValue("@Newowneremail", flattransfer.NewOwnerEmail);
          cmd.Parameters.AddWithValue("@Newowneradhar", flattransfer.NewOwnerAdhar);
          cmd.Parameters.AddWithValue("@FlatArea", flattransfer.FlatArea);
          cmd.Parameters.AddWithValue("@Transferfees", flattransfer.TransferFees);
          cmd.Parameters.AddWithValue("@PaymentMode", flattransfer.PaymentMode);
          cmd.Parameters.AddWithValue("@Identityproofurl", flattransfer.Identityproofurl ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@Agreement_copyurl", flattransfer.Agreementcopyurl ?? (object)DBNull.Value);

          int rowsAffected = await cmd.ExecuteNonQueryAsync(); // Async execution

          return rowsAffected > 0;
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error Adding New Flat Transfer with new Owner", e);
      }
      finally
      {
        if (_connection.State == ConnectionState.Open)
          _connection.Close();
      }
    }

  }



}

