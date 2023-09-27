using e_commerce.Repostories;
using e_commerce.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Azure;
using e_commerce.Classes;

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
        public String customerajax(Datatable datatable) {
            Customers customers = new Customers();
            Dictionary<String, Object> response = customers.getCustomersAjax(datatable);
            String json = JsonSerializer.Serialize(response);
            return json;
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
