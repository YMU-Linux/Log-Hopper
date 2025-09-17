using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogHopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MetricsController : ControllerBase
    {
        [HttpGet("prometheus")]
        public IActionResult GetPrometheus()
        {
            return Ok("Prometheus metrics");
        }

        [HttpPost("upload")]
        public IActionResult Upload()
        {
            return Ok("File uploaded");
        }
    }
}
