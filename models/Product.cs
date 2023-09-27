using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.models
{
    public class Product : Imodel
    {
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription{ get; set; }
        public double ProductPrice { get; set; }
        public double ProductOldPrice { get; set; }
        public String ProductImage { get; set; }



        public void add(SqlDataReader sdr) {
            ProductId = Convert.ToInt32(sdr["ProductId"]);
            ProductName = sdr["ProductName"].ToString();
            ProductDescription = sdr["ProductDescription"].ToString();
            ProductPrice = Convert.ToDouble(sdr["ProductPrice"]);
            ProductOldPrice = Convert.ToDouble(sdr["ProductOldPrice"]);
            ProductImage = sdr["ProductImage"].ToString();

        }

    }
}
