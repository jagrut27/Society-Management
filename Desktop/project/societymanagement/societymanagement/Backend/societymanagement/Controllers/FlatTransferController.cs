  using Microsoft.AspNetCore.Mvc;
  using societymanagement.Data;
  using societymanagement.Entity;

  namespace societymanagement.Controllers
  {
    [Route("Api/FlatTransfer")]
    [ApiController]
    public class FlatTransferController : Controller
    {

      private readonly FlatTransferRepository _repository;

      public FlatTransferController(FlatTransferRepository repository)
      {

        _repository=repository;
      }

      [HttpPost]
      public async Task<IActionResult> InsertFlatData([FromForm] FlatTransfer flat)
      {
        try
        {

          var result = await  _repository.InsertFlatTransfer(flat);

          if (result)
          {
            return Ok(new { success = true, message = "Flat transfer submitted successfully" });
          }
          else
          {
            return Ok(new { success = false, message = "Current Owner details is not match" });
          }


        }
        catch (Exception ex)
        {

          return StatusCode(500, new { success = false, message = $"Unexpected error: {ex.Message}" });
        }


      }



    }
  }
