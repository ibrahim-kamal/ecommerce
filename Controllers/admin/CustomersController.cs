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

            return View("~/Views/admin/Customers/list.cshtml");

        }
        public IActionResult manage(int id = 0)
        {
            Customers customers = new Customers();
            Customer customer = null;
            if (id != 0)
            {
                customer = customers.getById(id);
            }
            else
            {
                customer = new Customer();
            }
            return View("~/Views/admin/Customers/manage.cshtml" , customer);

        }
        [HttpPost]
        public ActionResult manage(Customer customer)
        {
            Customers customers = new Customers();
            if (customer.customerId == 0)
            {
                customer.customerId = customers.create(customer);
            }
            else
            {

                customer.customerId = customers.update(customer);
            }


            return RedirectToAction(nameof(list));
        }
        [HttpPost]
        public void delete(int id)
        {
            Customers customers = new Customers();
            customers.delete(id);

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
