using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.models
{
    public class Customer : Imodel
    {
        public int customerId { get; set; }
        public String customerName { get; set; }
        public String customerPhone { get; set; }
        public String customerEmail { get; set; }


        public void add(SqlDataReader sdr) {

            customerName = sdr["customerName"].ToString();
            customerPhone = sdr["customerPhone"].ToString();
            customerEmail = sdr["customerEmail"].ToString();
            customerId = Convert.ToInt32(sdr["customerId"]);
        }

    }
}
