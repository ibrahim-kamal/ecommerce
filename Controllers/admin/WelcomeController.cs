using e_commerce.Core;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers.admin
{
    public class WelcomeController : AdminController
    {
        public String Index()
        {
            return "Welcome Admin";
        }
    }
}
