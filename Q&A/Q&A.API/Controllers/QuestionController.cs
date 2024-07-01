using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        [HttpGet("QuestionList")]
        public async Task<IActionResult> GetQuestions()
        {
            try
            {
                List<Questions> questions = await Questions.GetAllQuestion();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("QuestionDetail/{questionID}")]
        public async Task<IActionResult> GetQuestionDetail(int questionID)
        {
            try
            {
                Questions question = await Questions.GetQuesById(questionID);
                if (question == null)
                {
                    return NotFound();
                }
                question.AnswersList = await Answers.GetAnsByQuesId(questionID);
                return Ok(question);
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            } 
        }

        [HttpPost("PostQestion")]
        public IActionResult PostQuestions([FromBody] Questions question)
        {
            int isSaved = Questions.SaveQuestion(question);
            if(isSaved>0)
            {
                return Ok(question);
            }
            return BadRequest("Failed to post the question");
        }
    }
}
