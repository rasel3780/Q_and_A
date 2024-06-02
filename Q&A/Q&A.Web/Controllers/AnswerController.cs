using Microsoft.AspNetCore.Mvc;

namespace Q_A.Web.Controllers
{
    public class AnswerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
