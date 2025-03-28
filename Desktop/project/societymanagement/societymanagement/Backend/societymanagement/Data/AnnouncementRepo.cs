using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;
using Razorpay.Api;
using societymanagement.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace societymanagement.Data
{
  public class AnnouncementRepo
  {
    private readonly string _connectionString;

    public AnnouncementRepo()
    {
      _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement_new;Integrated Security=True;TrustServerCertificate=True;";
    }
    public List<Announcement> GetAnnouncement()
    {
      List<Announcement> announcements = new List<Announcement>();
      using (var conn = new SqlConnection(_connectionString))
      {
        try
        {
          conn.Open();
          string query = "SELECT * FROM Announcement ORDER BY Date DESC";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              while (reader.Read())
              {
                announcements.Add(new Announcement
                {
                  Announcement_Id = Convert.ToInt32(reader["Announcement_Id"]),
                  Announcement_name = reader["Announcement_name"].ToString(),
                  Description = reader["Description"].ToString(), 
                  Date = Convert.ToDateTime(reader["Date"])
                });
              }
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine("Database error: " + ex.Message);
          throw; // Rethrow exception to see full error details
        }
      }
      return announcements;
    }
    }
  }
