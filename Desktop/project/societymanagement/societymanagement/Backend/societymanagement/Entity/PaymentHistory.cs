using Microsoft.AspNetCore.Server.HttpSys;

namespace societymanagement.Entity
{
  public class PaymentHistory
  {
    public int PaymentId { get; set; }
    public string Name { get; set; }
    public DateTime PaymentDate { get; set; }
    public string FlatNumber { get; set; }

    public string BlockNumber {get;set;}
    public decimal Amount { get; set; }
  

  }
}
