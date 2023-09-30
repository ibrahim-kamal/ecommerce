using e_commerce.Repostories;
using e_commerce.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using e_commerce.Classes;
using e_commerce.helpers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using e_commerce.viewModels;

namespace e_commerce.Controllers.admin
{
    public class ProductsController : Controller
    {

        private IHostingEnvironment Environment;

        public ProductsController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult list()
        {

            return View("~/Views/admin/Products/list.cshtml");

        }
        public IActionResult manage(int id = 0)
        {
            Products products = new Products();
            productViewModel product = null;
            if (id != 0)
            {
                product = products.getById(id);
            }
            else
            {
                product = new productViewModel();
            }
            return View("~/Views/admin/Products/manage.cshtml", product);

        }
        [HttpPost]
        public ActionResult manage(Product product , List<IFormFile> images, IFormFile ProductImage)
        {


            productViewModel productViewModel = null;
            Products products = new Products();
            if (product.ProductId != 0) { 
                Console.WriteLine("  product.ProductId => " + product.ProductId);
                productViewModel = products.getById(product.ProductId);
            }
            Images _images = new Images();
            Files files = new Files(this.Environment);
            if (ProductImage !=  null)
            {
                product.ProductImage = files.SaveImage(ProductImage, "uploads/products");
            }
            else if(product.ProductId != 0)
            { 
                product.ProductImage = productViewModel.ProductImage;
            }

            if (product.ProductId == 0)
            {
                product.ProductId = products.create(product);
            }
            else
            {


                products.update(product);
            }

            
            Image img = new Image();
            img.Fk_ProductId = product.ProductId;
            foreach (var image in images)
            {

                img.imageUrl = files.SaveImage(image, "uploads/products");
                _images.create(img);

            }

            return RedirectToAction(nameof(list));
        }
        [HttpPost]
        public void delete(int id)
        {
            Products products = new Products();
            products.delete(id);

        }
        [HttpPost]
        public void deleteImage(int id)
        {
            Images images = new Images();
            images.delete(id);

        }

        [HttpPost]
        public String productsajax(Datatable datatable) {
            Products products = new Products();
            Dictionary<String, Object> response = products.getDataAjax(datatable);
            String json = JsonSerializer.Serialize(response);
            return json;
        }


       



        

    }
}
