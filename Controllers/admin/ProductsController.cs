using e_commerce.Repostories;
using e_commerce.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using e_commerce.Classes;
using e_commerce.helpers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using e_commerce.viewModels;
using e_commerce.Core;
using Syncfusion.Pdf.Graphics.Images.Decoder;
using System.Formats.Asn1;
using CsvHelper;
using AutoMapper;
using System.Collections.Generic;
using System.Globalization;
using e_commerce.Excel;

namespace e_commerce.Controllers.admin
{
    public class ProductsController : AdminController
    {

        private IHostingEnvironment Environment;
        private readonly IMapper _mapper;

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


            e_commerce.models.Image img = new e_commerce.models.Image();
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



        [HttpPost]
        public ActionResult import(IFormFile sample)
        {

            Console.WriteLine("sample");

            using (var reader = new StreamReader(sample.OpenReadStream()))
            using (var csvr = new CsvReader(reader, CultureInfo.InvariantCulture))
            {


                IEnumerable<ProductExcel> products = csvr.GetRecords<ProductExcel>();
                foreach (ProductExcel prod in products)
                {
                    Products _products = new Products();
                    Product product = new Product();
                    product.ProductName = prod.ProductName;
                    product.ProductPrice = prod.ProductPrice;
                    product.ProductDescription = prod.ProductDescription;
                    Files files = new Files(this.Environment);
                    product.ProductImage = files.SaveImageFromLink(prod.ProductImage, "uploads/products");
                    _products.create(product);

                }


            }
            return RedirectToAction(nameof(list));



        }









    }
}
