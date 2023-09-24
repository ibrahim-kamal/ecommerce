using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
