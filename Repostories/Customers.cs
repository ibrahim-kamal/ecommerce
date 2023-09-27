using Azure;
using e_commerce.Classes;
using e_commerce.helpers;
using e_commerce.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Packaging.PackagingConstants;

namespace e_commerce.Repostories
{
    public class Customers
    {

        public List<Customer> getCustomers()
        {
            //List<Customer> customers = new List<Customer>();

            //SqlCommand cmd = db._db.SetCommand("select * from customer");
            List <Customer> customers = db._db.SetCommand<Customer>("select * from customer");

            //using (SqlDataReader sdr = cmd.ExecuteReader())
            //{
            //    while (sdr.Read())
            //    {
            //        customers.Add(new Customer
            //        {
            //            name = sdr["name"].ToString(),
            //            phone = sdr["phone"].ToString(),
            //            email = sdr["email"].ToString(),
            //            id = Convert.ToInt32(sdr["id"])
            //        });
            //    }
            //}

            return customers;
        }
    


        public List<Customer> getAjaxData(String searchQuery ,Dictionary<String , Object> filters, String status = "")
        {
            //List<Customer> result = new List<Customer>();
            String query = "select * from customer";
            if (searchQuery != ""){
                query += " where "+searchQuery+" ";
            }
            query += " order by "+filters["columnName"]+" "+filters["columnSortOrder"] +" ";
            query += " OFFSET "+filters["start"] +" ROWS ";
            query += " FETCH NEXT  "+filters["rowperpage"] +" ROWS ONLY";
            Console.WriteLine("query => " + query);
            //SqlCommand cmd = db._db.SetCommand(query);
            List<Customer> result =  db._db.SetCommand<Customer>(query);

            //using (SqlDataReader sdr = cmd.ExecuteReader())
            //{
            //    while (sdr.Read())
            //    {
            //        result.Add(new Customer
            //        {
            //            name = sdr["name"].ToString(),
            //            phone = sdr["phone"].ToString(),
            //            email = sdr["email"].ToString(),
            //            id = Convert.ToInt32(sdr["id"])
            //        });
            //    }
            //}
            return result;
        }



        public int totalRecords(String searchQuery, Dictionary<String, String> filters, String status = "")
        {

            String query = "select count(customerId) as c from customer";
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



        public Dictionary<String, Object> getCustomersAjax(Datatable postData)
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
                        "name like '%" + searchValue + "%'" +
                        " or email like '%" + searchValue + "%'" +
                        " or phone like '%" + searchValue + "%'" +
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





            List<Customer> data = getAjaxData(searchQuery, filters, "");



            Dictionary<String, Object> response = new Dictionary<String, Object>();


            response["draw"] = draw;
            response["iTotalRecords"] = totalRecords;
            response["iTotalDisplayRecords"] = totalRecordwithFilter;
            response["aaData"] = data;




            return response;
        }
    }
}
