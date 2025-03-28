using System.ComponentModel.DataAnnotations;

namespace Admin.Module
{
  public class FlatTransferRequest
  {
    [Key]
    public int Transfer_id { get; set; }
    public string Currentownername { get; set; }
    public string CurrentOwnercontactnumber { get; set; }
    public string FlatNumber { get; set; }
    public string BlockNumber { get; set; }
    public string NewOwnerFullName { get; set; }
    public string NewOwnerContactNumber { get; set; }
    public string NewOwnerEmail { get; set; }
    public string Flatarea { get; set; }
    public decimal transfer_fees { get; set; }
    public string paymentmode { get; set; }
    public string Identityproofurl { get; set; }
 
    public string Agreementcopyurl { get; set; }
    public string status { get; set; }
    public DateTime? RequestedAt { get; set; }  
    public DateTime? ApproveAt { get; set;  }

  }
}
