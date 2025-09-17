using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LogHopper.Models;

namespace LogHopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFiles()
        {
            return Ok(new[] { "report.pdf", "image.png" });
        }

        [HttpGet("{collections}/{filters}")]
        public IActionResult GetFiltered(string collections, string filters)
        {
            return Ok(new { collections, filters });
        }

        [HttpPost("Upload")]
public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file");

    string content;
    using var reader = new StreamReader(file.OpenReadStream());
    content = await reader.ReadToEndAsync();

    var fileModel = new FileModel
    {
        FileName = file.FileName,
        FileSize = file.Length,
        FileType = file.ContentType switch
        {
            "text/plain" => FileType.Text,
            "application/json" => FileType.Json,
            "application/pdf" => FileType.Pdf,
            _ => FileType.Unknown
        },
        ContentPreview = content.Length > 100 ? content.Substring(0, 100) : content
    };

    return Ok(fileModel);
}
    }
}
