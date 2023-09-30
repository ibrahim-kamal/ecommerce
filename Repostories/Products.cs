using Azure;
using e_commerce.Classes;
using e_commerce.helpers;
using e_commerce.models;
using e_commerce.viewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Packaging.PackagingConstants;

namespace e_commerce.Repostories
{
    public class Products
    {

        public List<Product> getAjaxData(String searchQuery ,Dictionary<String , Object> filters, String status = "")
        {

            String query = "select * from products";
            if (searchQuery != ""){
                query += " where "+searchQuery+" ";
            }
            query += " order by "+filters["columnName"]+" "+filters["columnSortOrder"] +" ";
            query += " OFFSET "+filters["start"] +" ROWS ";
            query += " FETCH NEXT  "+filters["rowperpage"] +" ROWS ONLY";
            Console.WriteLine("query => " + query);

            List<Product> result =  db._db.SetCommand<Product>(query);


            return result;
        }



        public int totalRecords(String searchQuery, Dictionary<String, String> filters, String status = "")
        {

            String query = "select count(productId) as c from products";
            if (searchQuery != "")
            {
                query += " where " + searchQuery + " ";
            }

            Console.WriteLine("query => " + query);

            List<TotalRows> total = db._db.SetCommand<TotalRows>(query);

            //SqlDataReader sdr = cmd.ExecuteReader();
            //sdr.Read();
            //int count = Convert.ToInt32(sdr["c"]);
            return total[0].c ;
        }



        public Dictionary<String, Object> getDataAjax(Datatable postData)
        { 
            





            int draw = postData.draw;

            int start = postData.start;

            int rowperpage = postData.length; // Rows display per page

            int columnIndex = postData.order[0].column;

            String columnName = postData.columns[columnIndex].data; // Column name

            String columnSortOrder = postData.order[0].dir; // asc or desc

            String searchValue = postData.search.value; // Search value




            String searchQuery = "";

            if (!string.IsNullOrEmpty(searchValue))
            {

                searchQuery = "(" +
                        "productName like '%" + searchValue + "%'" +
                        " or productDescription like '%" + searchValue + "%'"+
                        ")";


            }




            int totalRecordwithFilter = this.totalRecords(searchQuery, null , "");





            int totalRecords = this.totalRecords("", null , "");



            Dictionary<String ,Object> filters = new Dictionary<String ,Object>();  


            filters["searchQuery"] = searchQuery;
            filters["columnName"] = columnName;
            filters["columnSortOrder"] = columnSortOrder;
            filters["rowperpage"] = rowperpage;
            filters["start"] = start;





            List<Product> data = getAjaxData(searchQuery, filters, "");



            Dictionary<String, Object> response = new Dictionary<String, Object>();


            response["draw"] = draw;
            response["iTotalRecords"] = totalRecords;
            response["iTotalDisplayRecords"] = totalRecordwithFilter;
            response["aaData"] = data;




            return response;
        }



        public int create(Product product) {
            int id = db._db.insert("insert into products" +
                "(ProductName , ProductDescription , ProductPrice ,  ProductOldPrice , ProductImage) " +
                "values" +
                "('" + product.ProductName + "','" + product.ProductDescription + "' , " + product.ProductPrice+ " , " + product.ProductOldPrice + ", '" + product.ProductImage + "')");
            return id;
        }
        public int update(Product product) {
            int updated = db._db.update("update products set " +
                "ProductName = '" + product.ProductName+ "' ," +
                "ProductDescription = '" + product.ProductDescription+ "' , " +
                "ProductPrice = '" + product.ProductPrice+ "', " +
                "ProductOldPrice = '" + product.ProductOldPrice+ "', " +
                "ProductImage = '" + product.ProductImage + "' " +
                "where ProductId = " + product.ProductId+
                "");
            return updated;
        }


        public void delete(int Id)
        {

            db._db.update("delete from products where ProductId = " + Id);

            
        }
        public productViewModel getById(int Id) {

            String Query = "select * from products" +
                " left join Images on Fk_ProductId = ProductId" +
                " where ProductId = " + Id;

            List<productImage> products = db._db.SetCommand<productImage>(Query);
            
            productViewModel product = new productViewModel();

            product.ProductName  = products[0].product.ProductName;
            product.ProductId = products[0].product.ProductId;
            product.ProductImage = products[0].product.ProductImage;
            product.ProductDescription = products[0].product.ProductDescription;
            product.ProductPrice = products[0].product.ProductPrice;
            product.ProductOldPrice = products[0].product.ProductOldPrice;
            product.images = new List<Image>();
            foreach (var productImage in products)
            {
                Image image = new Image();
                image.Fk_ProductId = productImage.image.Fk_ProductId;
                image.imageUrl = productImage.image.imageUrl;
                image.imageId = productImage.image.imageId;
                product.images.Add(image);
                
            }
            return product;
        }
    }
}
