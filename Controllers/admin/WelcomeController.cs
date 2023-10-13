using e_commerce.Core;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers.admin
{
    public class WelcomeController : AdminController
    {
        [Route("admin")]
        public String Index()
        {
            return "Welcome Admin";
        }
    }
}
