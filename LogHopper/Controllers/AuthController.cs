using Microsoft.AspNetCore.Mvc;

namespace LogHopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] dynamic payload) 
            => Ok("Login endpoint");

        [HttpPost("reset")]
        public IActionResult Reset() 
            => Ok("Password reset endpoint");

        [HttpPost("logout")]
        public IActionResult Logout() 
            => Ok("Logout endpoint");
    }
}
