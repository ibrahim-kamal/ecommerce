using e_commerce.Classes.datatable;
using e_commerce.models;
using Microsoft.Data.SqlClient;

namespace e_commerce.viewModels
{
    public class OrderDetailsViewModel
    {
        public models.Order order { get; set; }
        public List<orderDetails> orderDetails { get; set; }
        public List<Product> products { get; set; }
        public List<Customer> customers{ get; set; }
        public Customer customer{ get; set; }


    }
}
