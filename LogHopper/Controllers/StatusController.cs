using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace LogHopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
       
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "ok", time = DateTime.UtcNow });
        }
    }
}
