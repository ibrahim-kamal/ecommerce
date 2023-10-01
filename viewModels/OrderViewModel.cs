using e_commerce.Classes.datatable;
using e_commerce.models;
using Microsoft.Data.SqlClient;

namespace e_commerce.viewModels
{
    public class OrderViewModel : Imodel
    {
        public models.Order order { get; set; }
        public Customer customer{ get; set; }

        public void add(SqlDataReader sdr)
        {
            order = new models.Order();
            order.add(sdr);
            customer  = new Customer();
            customer.add(sdr);
            
        }
    }
}
