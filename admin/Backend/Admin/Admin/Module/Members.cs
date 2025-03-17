using System.ComponentModel.DataAnnotations;

namespace Admin.Module
{
  public class Members
  {
    [Key]
    public int MemberId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }  
    public string PhoneNumber { get; set; }

    public string FlatNumber { get; set; }
    public string BlockNumber { get; set; }
    public string role { get; set; }

  }
}
