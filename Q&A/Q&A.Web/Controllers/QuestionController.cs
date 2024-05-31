using Microsoft.AspNetCore.Mvc;

namespace Q_A.Web.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
