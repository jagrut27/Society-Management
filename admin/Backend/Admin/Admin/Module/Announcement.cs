using System.ComponentModel.DataAnnotations;

namespace Admin.Module
{
  public class Announcement  
  {
    [Key]
    public int Announcement_id { get; set; }
    public string Announcement_name { get; set; }

    public string Description { get; set; }

    public DateTime date { get; set; }
  }
}
