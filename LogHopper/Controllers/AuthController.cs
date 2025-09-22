using LogHopper.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace LogHopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MariaDBController _db;

        public AuthController(MariaDBController db)
        {
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest payload)
        {
            if (payload == null ||
                string.IsNullOrWhiteSpace(payload.Username) ||
                string.IsNullOrWhiteSpace(payload.Password))
            {
                return BadRequest("Username and password are required.");
            }

            string sql = "SELECT RolePower FROM Users WHERE Username = @username AND Password = @password";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@username", payload.Username),
                new MySqlParameter("@password", payload.Password)
            };

            DataTable result = await _db.ReadAsync(sql, parameters);

            if (result.Rows.Count > 0)
            {
                string role = result.Rows[0]["RolePower"].ToString();
                return Ok(new { Success = true, Role = role});
                
            }

            return Unauthorized("Invalid username or password.");
        }

        [HttpPost("reset")]
        public IActionResult Reset()
            => Ok(new {Reset = true});

        [HttpPost("logout")]
        public IActionResult Logout()
            => Ok(new {Logout = true});
    }
}
