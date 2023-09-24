using e_commerce.helpers;
using e_commerce.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Packaging.PackagingConstants;

namespace e_commerce.Repostories
{
    public class Customers
    {

        public List<Customer> getCustomers()
        {
            List<Customer> customers = new List<Customer>();

            SqlCommand cmd = db._db.SetCommand("select * from customer");

            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    customers.Add(new Customer
                    {
                        name = sdr["name"].ToString(),
                        phone = sdr["phone"].ToString(),
                        email = sdr["email"].ToString(),
                        id = Convert.ToInt32(sdr["id"])
                    });
                }
            }

            return customers;
        }
    


        public List<Customer> getAjaxData(String searchQuery ,Dictionary<String , String> filters, String status = "")
        {
            List<Customer> result = new List<Customer>();
            String query = "select * from customers";
            if (searchQuery != ""){
                query += " where "+searchQuery+" ";
            }
            query += " order by "+filters["columnName"]+" "+filters["columnSortOrder"] +" ";
            query += " limit "+filters["start"] +" , "+filters["rowperpage"] +" ";

            SqlCommand cmd = db._db.SetCommand(query);

            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    result.Add(new Customer
                    {
                        name = sdr["name"].ToString(),
                        phone = sdr["phone"].ToString(),
                        email = sdr["email"].ToString(),
                        id = Convert.ToInt32(sdr["id"])
                    });
                }
            }
            return result;
        }



        public int totalRecords(String searchQuery, Dictionary<String, String> filters, String status = "")
        {

            String query = "select count(id) as c from customers";
            if (searchQuery != "")
            {
                query += " where " + searchQuery + " ";
            }
            query += " order by " + filters["columnName"] + " " + filters["columnSortOrder"] + " ";
            query += " limit " + filters["start"] + " , " + filters["rowperpage"] + " ";

            SqlCommand cmd = db._db.SetCommand(query);

            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            return Convert.ToInt32(sdr["c"]);     
        }



        public void getCustomersAjax() { 
    
        }
    }
}
