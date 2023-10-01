using e_commerce.Repostories;
using e_commerce.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using e_commerce.Classes;
using e_commerce.helpers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using e_commerce.viewModels;
using e_commerce.Core;

namespace e_commerce.Controllers.admin
{
    public class OrdersController : AdminController
    {

        private IHostingEnvironment Environment;

        public OrdersController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult list()
        {

            return View("~/Views/admin/Orders/list.cshtml");

        }
        public IActionResult manage(int id = 0)
        {
            Orders orders = new Orders();
            Products products = new Products();
            Customers customers = new Customers();
            OrderDetailsViewModel orderDetailsViewModel = null;
            if (id != 0)
            {
                orderDetailsViewModel = orders.getById(id);
            }
            else
            {
                orderDetailsViewModel = new OrderDetailsViewModel();
                orderDetailsViewModel.order = new Order();

            }
            orderDetailsViewModel.products = products.getAll();
            orderDetailsViewModel.customers = customers.getAll();
            return View("~/Views/admin/Orders/manage.cshtml", orderDetailsViewModel);

        }
        [HttpPost]
        public IActionResult manage(OrderRequest orderrequest)
        {


            Orders orders = new Orders();
            OrderDetailsRepostories orderDetailsRepostories = new OrderDetailsRepostories();
            orderrequest.order.Fk_userId = 0;

            if (orderrequest.order.orderId == 0)
            {
                orderrequest.order.orderId = orders.create(orderrequest.order);
            }
            else
            {


                orders.update(orderrequest.order);
            }



            orderDetailsRepostories.delete(orderrequest.order.orderId);
            foreach ( var orderDetails in orderrequest.orderDetails)
            {
                orderDetails OrderDetailsRow = orderDetails.Value;
                OrderDetailsRow.orderDetailsId = orderrequest.order.orderId;
                orderDetailsRepostories.create(OrderDetailsRow);

            }

            return RedirectToAction(nameof(list));
        }
        [HttpPost]
        public void delete(int id)
        {
            Orders orders = new Orders();
            orders.delete(id);

        }
        [HttpPost]
        public void deleteImage(int id)
        {
            Images images = new Images();
            images.delete(id);

        }

        [HttpPost]
        public String Ordersajax(Datatable datatable) {
            Orders orders = new Orders();
            Dictionary<String, Object> response = orders.getDataAjax(datatable);
            String json = JsonSerializer.Serialize(response);
            return json;
        }



        public String test()
        {
            OrderViewModel response = new OrderViewModel();
            response.order = new Order();
            response.customer = new Customer();
            String json = JsonSerializer.Serialize(response);
            return json;
        }







    }
}
