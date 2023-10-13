using e_commerce.Repostories;
using e_commerce.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using e_commerce.Classes;
using e_commerce.helpers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using e_commerce.viewModels;
using e_commerce.Core;


using IronPdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DinkToPdf.Contracts;
using Razor.Templating.Core;
using Microsoft.Extensions.Hosting;
using Azure.Core;
using OfficeOpenXml;

namespace e_commerce.Controllers.admin
{
    public class OrdersController : AdminController
    {

        private IWebHostEnvironment Environment;
        private readonly IConverter _converter;

        public OrdersController(IWebHostEnvironment _environment , IConverter converter)
        {
            Environment = _environment;
            _converter = converter;
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
                orderDetailsViewModel.orderDetails = new List<orderDetails>();
                orderDetailsViewModel.orderDetails.Add(new orderDetails());

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
            foreach (var orderDetails in orderrequest.orderDetails)
            {
                orderDetails OrderDetailsRow = orderDetails.Value;
                OrderDetailsRow.Fk_orderId = orderrequest.order.orderId;
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
        public String Ordersajax(Datatable datatable)
        {
            Orders orders = new Orders();
            Dictionary<String, Object> response = orders.getDataAjax(datatable);
            String json = JsonSerializer.Serialize(response);
            return json;
        }




        public async Task<IActionResult> pdf(int id)
        {

            Orders orders = new Orders();
            OrderDetailsViewModel orderDetailsViewModel = orders.getById(id); ;

            string wwwrootPath = Environment.WebRootPath;
            string hostname = Request.Host.Host;
            Dictionary<String, Object> viewData = new Dictionary<string, object>();
            String baseUrl = $"{Request.Scheme}://{Request.Host}";
            viewData.Add("baseurl", baseUrl);
            var viewPdf = await RazorTemplateEngine.RenderAsync("~/Views/admin/Orders/View.cshtml", orderDetailsViewModel, viewData);

            byte[]  fileBytes = PDF.Generate(viewPdf , _converter);

            Response.Headers.ContentType = "application/pdf";
            return File(fileBytes, "application/pdf");


        }


        public IActionResult export()
        {
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Create a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                // get All Data

                Orders orders = new Orders();
                List<OrderViewModel> allorder = orders.getAll();

                // Set cell values
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Customer Name";
                worksheet.Cells[1, 3].Value = "Total";
                worksheet.Cells[1, 4].Value = "Discount";
                worksheet.Cells[1, 5].Value = "Status";
                int i = 2;
                foreach (OrderViewModel _order in allorder)
                {
                    worksheet.Cells[i, 1].Value = _order.order.orderId;
                    worksheet.Cells[i, 2].Value = _order.customer.customerName;
                    worksheet.Cells[i, 3].Value = _order.order.orderTotal;
                    worksheet.Cells[i, 4].Value = _order.order.orderDiscount;
                    worksheet.Cells[i, 5].Value = _order.order.orderStatus;
                    i += 1;

                }

                // Generate the Excel file stream
                var stream = new MemoryStream(package.GetAsByteArray());

                // Set the response headers
                Response.Headers.Add("Content-Disposition", "attachment; filename=export.xlsx");
                Response.Headers.Add("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                // Return the file stream as the response
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }





    }
}
