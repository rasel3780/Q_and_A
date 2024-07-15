using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("UserQuestions/{userId}")]
        public async Task<IActionResult> GetQuestionsByUserId(int userId)
        {
            var questions = await Questions.GetQuestionsByUserId(userId);
            if (questions == null || questions.Count == 0)
            {
                return NotFound(new { Message = "No questions found for this user." });
            }
            return Ok(questions);
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

        [Authorize]
        [HttpPost("PostQuestion")]
        public IActionResult PostQuestions([FromBody] Questions question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid model state", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                int isSaved = Questions.SaveQuestion(question);
                if (isSaved > 0)
                {
                    return Ok(question);
                }
                return BadRequest(new { Message = "Failed to save question" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request", Error = ex.Message });
            }
        }

        
    }
}
