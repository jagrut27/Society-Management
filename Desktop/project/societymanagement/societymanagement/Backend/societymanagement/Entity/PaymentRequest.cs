namespace societymanagement.Entity
{
  public class PaymentRequest
  {

    public int MemberId { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string FlatNumber { get; set; }
    public string BlockNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }

    public string Status { get; set; }
    //   public string orderId { get; set; }
    //  public string RazorpayPaymentId { get; set; }
  }
}
