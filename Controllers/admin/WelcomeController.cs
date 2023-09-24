using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers.admin
{
    public class WelcomeController : Controller
    {
        public String Index()
        {
            return "Welcome Admin";
        }
    }
}
