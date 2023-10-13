using e_commerce.models;
using Microsoft.Data.SqlClient;

namespace e_commerce.Classes
{
    public class productImage : Imodel
    {
        public Product product{ get; set; }
        public models.Image image { get; set; }

        public void add(SqlDataReader sdr)
        {
            product = new Product();
            product.add(sdr);
            image = new models.Image();
            image.add(sdr);

        }
    }
}
