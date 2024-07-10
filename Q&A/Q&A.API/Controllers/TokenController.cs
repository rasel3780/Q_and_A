using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly JwtService _jwtService;

        public TokenController(ILogger<TokenController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult GetServiceToken()
        {
            var defualtUser = new Users
            {
                UserID = 0,
                UserName = "Guest"
            };
            var token = _jwtService.GenerateToken(defualtUser);
            return Ok(new { token });
        }
    }
}
