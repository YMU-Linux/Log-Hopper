using Microsoft.AspNetCore.Mvc;

namespace LogHopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDocs()
        {
            return Ok("API Documentation");
        }
    }
}
