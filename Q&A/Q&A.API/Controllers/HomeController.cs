using Microsoft.AspNetCore.Mvc;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet(Name = "PName")]
        public IActionResult Name()
        {
            return Ok("Q&A");
        }
    }
}
