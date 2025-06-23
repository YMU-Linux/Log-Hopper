using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok("API is running!");
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest data)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();

            // Here you can implement your actual login and token generation logic
            var token = "your-jwt-token";

            Response.Headers["Authorization"] = "Bearer " + token;

            return Ok(new
            {
                Message = "Login received!",
                Username = data.Username,
                Password = data.Password,
                UserAgent = userAgent,
                Token = token
            });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to the Log-Hopper API!");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
