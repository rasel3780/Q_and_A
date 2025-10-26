using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q_A.API.Model;

namespace Q_A.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Categories> categories = await Categories.GetAllCategory();
            return Ok(categories);
        }
    }
}
