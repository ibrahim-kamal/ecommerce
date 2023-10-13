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
    public class Orders
    {

        public List<OrderViewModel> getAll()
        {

            String query = "" +
                "select * from orders " +
                "join Customer on Fk_customerId = customerId";

            List<OrderViewModel> result = db._db.SetCommand<OrderViewModel>(query);


            return result;
        }
        public List<OrderViewModel> getAjaxData(String searchQuery ,Dictionary<String , Object> filters, String status = "")
        {

            String query = "" +
                "select * from orders " +
                "join Customer on Fk_customerId = customerId";
            if (searchQuery != ""){
                query += " where "+searchQuery+" ";
            }
            query += " order by "+filters["columnName"]+" "+filters["columnSortOrder"] +" ";
            query += " OFFSET "+filters["start"] +" ROWS ";
            query += " FETCH NEXT  "+filters["rowperpage"] +" ROWS ONLY";
            Console.WriteLine("query => " + query);

            List<OrderViewModel> result =  db._db.SetCommand<OrderViewModel>(query);


            return result;
        }



        public int totalRecords(String searchQuery, Dictionary<String, String> filters, String status = "")
        {

            String query = "select count(orderId) as c from orders";
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
                        "orderName like '%" + searchValue + "%'" +
                        " or orderDescription like '%" + searchValue + "%'"+
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





            List<OrderViewModel> data = getAjaxData(searchQuery, filters, "");



            Dictionary<String, Object> response = new Dictionary<String, Object>();


            response["draw"] = draw;
            response["iTotalRecords"] = totalRecords;
            response["iTotalDisplayRecords"] = totalRecordwithFilter;
            response["aaData"] = data;




            return response;
        }



        public int create(Order order) {
            int id = db._db.insert("insert into orders" +
                "(Fk_customerId , Fk_userId , orderSubTotal , address, orderTotal ,   orderDiscount ,  OrderVat , orderStatus) " +
                "values" +
                "('" + 
                order.Fk_customerId + "','" +
                order.Fk_userId + "','" +
                order.orderSubTotal + "','" +
                order.address + "','" +
                order.orderTotal + "' , " + order.orderDiscount+ " , " + order.OrderVat + ", '" + order.orderStatus + "')");
            return id;
        }
        public int update(Order order) {
            int updated = db._db.update("update orders set " +
                "Fk_customerId = '" + order.Fk_customerId + "' ," +
                "orderTotal = '" + order.orderTotal+ "' , " +
                "orderDiscount = '" + order.orderDiscount+ "', " +
                "OrderVat = '" + order.OrderVat+ "', " +
                "orderStatus = '" + order.orderStatus+ "' " +
                "where OrderId = " + order.orderId+
                "");
            return updated;
        }


        public void delete(int Id)
        {

            db._db.update("delete from orders where OrderId = " + Id);

            
        }
        public OrderDetailsViewModel getById(int Id) {

            String Query = "select * from orders" +
                " join orderDetails on Fk_orderId = OrderId" +
                " join products on FK_productId = ProductId" +
                " where orderId = " + Id;

            List<OrderDetails> orders = db._db.SetCommand<OrderDetails>(Query);

            OrderDetailsViewModel order = new OrderDetailsViewModel();


            order.order = new Order(orders[0].order);
            Customers customers = new Customers();
            order.orderDetails = new List<orderDetails>();
            order.customer = customers.getById(orders[0].order.Fk_customerId);
            foreach (var orderDetails in orders)
            {
                orderDetails details = new orderDetails(orderDetails._orderDetails);
                order.orderDetails.Add(details);
            }
            return order;
        }
    }
}
