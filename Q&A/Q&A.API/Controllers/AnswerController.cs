using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        [HttpGet("GetAnswersByQuestion/{quesId}")]
        public IActionResult GetAnswersByQuesId(int quesId)
        {
            List<Answers> answers = Answers.GetAnsByQuesId(quesId);
            return Ok(answers);
        }
    }
}
