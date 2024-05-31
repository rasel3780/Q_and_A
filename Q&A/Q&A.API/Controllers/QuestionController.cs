using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

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
    }
}
