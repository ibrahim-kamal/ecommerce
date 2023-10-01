using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.models
{
    public class ProductPrderDetails
    {

        Product product { set; get; }


        public void add(SqlDataReader sdr) 
        {
            product = new Product();
            product.add(sdr);

        }

    }
}
