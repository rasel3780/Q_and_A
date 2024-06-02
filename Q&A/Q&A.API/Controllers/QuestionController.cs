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

        //[HttpGet("QuestionDetail/{questionID}")]
        //public IActionResult GetQuestionDetail(int quesId)
        //{
        //    Questions question = Questions.GetQuesById(quesId);
        //    question.AnswersList = Answers.GetAnsByQuesId(quesId);

        //    return Ok(question);
        //}
    }
}
