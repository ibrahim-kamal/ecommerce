using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.Excel
{
    public class ProductExcel
    {
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription{ get; set; }
        public double ProductPrice { get; set; }
        public String ProductImage { get; set; }


    }
}
