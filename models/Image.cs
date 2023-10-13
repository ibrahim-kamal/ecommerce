using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.models
{
    public class Image : Imodel
    {
        public int imageId { get; set; }
        public String imageUrl { get; set; }
        public int Fk_ProductId { get; set; }


        public void add(SqlDataReader sdr) {
            if (DBNull.Value != sdr["imageId"]) {
                imageId = Convert.ToInt32(sdr["imageId"]);
                Fk_ProductId = Convert.ToInt32(sdr["Fk_ProductId"]);
                imageUrl = sdr["imageUrl"].ToString();
            }
        }

    }
}
