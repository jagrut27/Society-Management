using Microsoft.AspNetCore.Mvc;
using societymanagement.Data;
using societymanagement.Entity;

namespace societymanagement.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : Controller
    {
       private readonly ImageRepository _imageRepository;       

        public ImageController(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> UploadImage([FromForm] int? memberId, [FromForm] IFormFile file)
        //{
        //    if (memberId == null || memberId <= 0)
        //        return BadRequest("Invalid member ID.");

        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        //    var extension = Path.GetExtension(file.FileName).ToLower();
        //    if (!allowedExtensions.Contains(extension))
        //        return BadRequest("Only JPG and PNG files are allowed.");

        //    if (file.Length > 5 * 1024 * 1024) // 5MB limit
        //        return BadRequest("File size exceeds the 5MB limit.");

        //    // ✅ Custom Image Upload Path
        //    string uploadsFolder = @"C:\Users\pcit99.PRUDENT\Desktop\pratham\.net\ado.net\societymanagement\societymanagement\imageupload";

        //    // Create folder if not exists
        //    if (!Directory.Exists(uploadsFolder))
        //        Directory.CreateDirectory(uploadsFolder);

        //    // Generate unique file name
        //    string uniqueFileName = Guid.NewGuid().ToString() + extension;
        //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    // Save file
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }

        //    // File URL (Local file path, not accessible via browser)
        //    string fileUrl = filePath;
            
        //    // Update database with the file path
        //    _memberService.UpdateMemberImage(memberId.Value, fileUrl);

        //    return Ok(new { Message = "Image uploaded successfully!", ImagePath = fileUrl });
        //}

    }
}
