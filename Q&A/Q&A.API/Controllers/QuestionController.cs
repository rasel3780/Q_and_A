using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        [HttpGet("QuestionList")]
        public IActionResult GetQuestions()
        {
            List<Questions> questions = Questions.GetAllQuestion();
            return Ok(questions);
        }

        [HttpGet("QuestionDetail/{questionID}")]
        public IActionResult GetQuestionDetail(int questionID)
        {
            Questions question = Questions.GetQuesById(questionID);
            if (question == null) 
            {
                return NotFound();
            }

            return Ok(question);
        }
    }
}
