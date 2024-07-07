using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Users user)
        {
            var userFound = Users.AuthenticateUser(user.UserName, user.Password);
            if (userFound != null)
            {
                return Ok(new { userID = userFound.UserID, userName = userFound.UserName });
            }
            return Unauthorized();
        }
    }
}
