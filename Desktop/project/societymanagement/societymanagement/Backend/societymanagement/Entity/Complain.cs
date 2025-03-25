namespace societymanagement.Entity
{
  public class Complain
  {
    //public int ComplaintId { get; set; }
    public int MemberId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Status { get; set; } = "pending";
    public DateTime? CreatedAt { get; set; }
  }
}
