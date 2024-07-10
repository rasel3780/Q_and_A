using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly JwtService _jwtService;

        public UserController(ILogger<UserController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login loginData)
        {
            _logger.LogInformation("Login request with username:{username}", loginData.UserName);
            
            var userFound = Users.AuthenticateUser(loginData);
            if (userFound != null)
            {
                var token = _jwtService.GenerateToken(userFound);
                return Ok(new { token, userID = userFound.UserID, userName = userFound.UserName });
            }
            return Unauthorized();
        }



        [HttpGet("CheckUnique")]
        public IActionResult CheckUnique(string field, string value)
        {
            bool isUnique = Users.IsFieldUnique(field, value);
            return Ok(new { isUnique = isUnique });
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Users user)
        {
            try
            {
                int result = Users.Register(user);
                if (result > 0)
                {
                    return Ok(new { success = true });
                }
                return BadRequest(new { success = false, message = "Registration failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}
