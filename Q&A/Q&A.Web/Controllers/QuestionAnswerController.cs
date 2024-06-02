using Microsoft.AspNetCore.Mvc;

namespace Q_A.Web.Controllers
{
    public class QuestionAnswerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int quesID)
        {
            ViewBag.quesID = quesID;
            return View();
        }
    }
}
