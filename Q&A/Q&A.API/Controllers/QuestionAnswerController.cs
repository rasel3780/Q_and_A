using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        [HttpGet("QuestionList")]
        public IActionResult GetQuestions()
        {
            List<Questions> questions = Questions.GetAllQuestion();
            return Ok(questions);
        }

        [HttpGet("GetAnswers/quesId")]
        public IActionResult GetAnswers(int quesId)
        {
            List<Answers> answers = Answers.GetAnsByQuesId(quesId);
            return Ok(answers);
        }
    }
}
