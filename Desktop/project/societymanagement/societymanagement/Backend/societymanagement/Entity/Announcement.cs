namespace societymanagement.Entity
{
  public class Announcement
  {
    public int Announcement_Id { get; set; }
    public string Announcement_name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    internal static void Add(Announcement announcement)
    {
      throw new NotImplementedException();
    }
  }
}
