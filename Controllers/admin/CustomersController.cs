using e_commerce.Repostories;
using e_commerce.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace e_commerce.Controllers.admin
{
    public class CustomersController : Controller
    {

        public IActionResult list()
        {
            Customers customers = new Customers();
            List<Customer> customersList = customers.getCustomers();

            return View("~/Views/admin/Customers/list.cshtml", customersList);

        }

        [HttpPost]
        public void customerajax(IFormCollection collection) {
            Console.WriteLine(HttpContext.Request.Form["test"]);
            Console.WriteLine(collection["test"]);
            //Console.WriteLine(collection.test);
        }

        public IActionResult index()
        {
            Customers customers = new Customers();
            List<Customer> customersList = customers.getCustomers();

            return View("~/Views/admin/Customers/index.cshtml", customersList);

        }

        public string getAjax()
        {
            Customers customers = new Customers();
            List<Customer> customersList = customers.getCustomers();
            Dictionary<String, Object> response = new Dictionary<string, object>();
            response.Add("status", 200);
            response.Add("message", "CustomerList");
            response.Add("data", customersList);
            
            String json = JsonSerializer.Serialize(response);
            return json;
        }


        

    }
}
