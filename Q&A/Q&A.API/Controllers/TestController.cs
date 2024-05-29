using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

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

        [HttpGet(Name ="ProjectType")]
        public IActionResult ProjectType()
        {
            return Ok("api based");
        }

        [HttpPost(Name = "SaveName")]
        public IActionResult SaveName([FromBody]Questions questions)
        {
            return Ok(new{status="success", output=questions.Title});
        }

        [HttpGet]
        public IActionResult GetConString()
        {
            string conString = DbConnection.GetDbConString();
            return Ok(new {status="success", output= conString });
        }

    }
}
