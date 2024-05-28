using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet (Name = "ProjectName")]
        public IActionResult ProjectName()
        {
            return Ok("Q&A project");
        }

        [HttpGet(Name = "ProjectType")]
        public IActionResult ProjectType()
        {
            return Ok("api based");
        }

    }
}
