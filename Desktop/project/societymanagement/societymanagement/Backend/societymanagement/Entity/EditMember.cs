using Microsoft.AspNetCore.Mvc;

namespace societymanagement.Entity
{
  public class EditMember
  {

    public int MemberId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    //public string Email { get; set; }
    //public string Password { get; set; }  // Consider hashing passwords before storing
    public string PhoneNumber { get; set; }

    public string FlatNumber { get; set; }
    public string BlockNumber { get; set; }

    public String? ImageUrl { get; set; }

    [FromForm]
    public IFormFile? ImageFile { get; set; }

  


  }
}
