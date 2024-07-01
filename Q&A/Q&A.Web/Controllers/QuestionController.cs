using Microsoft.AspNetCore.Mvc;

namespace Q_A.Web.Controllers
{
    [Route("Question")]
    public class QuestionController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Detail/{questionID}")]
        public IActionResult Details(int questionID)
        {
            ViewBag.quesID = questionID;
            return View();
        }
    }
}
