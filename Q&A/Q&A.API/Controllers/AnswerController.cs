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
        public async Task<IActionResult> GetAnswersByQuesId(int quesId)
        {
            try
            {
                List<Answers> answers = await Answers.GetAnsByQuesId(quesId);
                return Ok(answers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
