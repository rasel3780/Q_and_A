using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("PostAnswer")]
        public IActionResult PostAnswer([FromBody] Answers answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid model state", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }
            try
            {
                answer.MakeDate = DateTime.UtcNow;
                int isSaved = Answers.SaveAnswer(answer);
                if (isSaved > 0)
                {
                    return Ok(answer);
                }
                return BadRequest(new { Message = "Failed to save answer" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request", Error = ex.Message });
            }
        }
    }
}
