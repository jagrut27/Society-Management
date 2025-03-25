using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace societymanagement.Entity
{
  public class FlatTransfer
  {





    [Required]
    public string CurrentOwnerName { get; set; }

    [Required]
    public string CurrentOwnerContactNumber { get; set; }

    [Required]
    public string FlatNumber { get; set; }

    [Required]
    public string BlockNumber { get; set; }

    [Required]
    public string NewOwnerFullName { get; set; }

    [Required]
    public string NewOwnerContactNumber { get; set; }

    [Required]
    public string NewOwnerEmail { get; set; }

    [Required]
    public string NewOwnerAdhar { get; set; }

    [Required]
    public string FlatArea { get; set; }



    [Required]
    public decimal TransferFees { get; set; }

    [Required]
    public string PaymentMode { get; set; }

    public IFormFile ImageIdentityproofFile { get; set; }
    public string? Identityproofurl { get; set; }

    public IFormFile Agreementcopyfile { get; set; }
    public string? Agreementcopyurl { get; set; }






  }


}

