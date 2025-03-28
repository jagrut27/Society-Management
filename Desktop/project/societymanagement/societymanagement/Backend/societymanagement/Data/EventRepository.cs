using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using societymanagement.Entity;

namespace societymanagement.Data
{
  public class EventRepository
  {
    private readonly SqlConnection _connection;


    public EventRepository()
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


    public List<Event>  GetEvent()
    {
      List<Event> events = new List<Event>();

        try
        {
          SqlCommand cmd = new SqlCommand("GetEventByUser", _connection);
          cmd.CommandType=CommandType.StoredProcedure;

          SqlDataAdapter dataAdapter=new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          dataAdapter.Fill(dt);

          foreach (DataRow row in dt.Rows) {

            Event e = new Event
            {
              EventName = row["event_name"].ToString(),
              Description = row["Description"].ToString(),
              EventDate = row["event_date"] != DBNull.Value
                                        ? DateOnly.FromDateTime(Convert.ToDateTime(row["event_date"]))
                                        : (DateOnly?)null,
              Place = row["place"].ToString(),
              EventTime = row["event_time"].ToString()

            };

            events.Add(e);
          

          }


      }
      catch (Exception ex) {

        throw new Exception("Error Fetching Events",ex);
      }
      return events;

    }

  }
}
