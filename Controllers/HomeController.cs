using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
