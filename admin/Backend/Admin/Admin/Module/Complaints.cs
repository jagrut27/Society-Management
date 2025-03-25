using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Admin.Module
{
  public class Complaints
  {
    public int complaints_id { get; set; }
    public int member_id { get; set; }

    public string Firstname { get;set; }
    public string Lastname { get; set; }
    public string FlatNumber { get; set; }
    public string BlockNumber { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime  created_at { get; set; }
    public object Members { get; internal set; }
  }
}
