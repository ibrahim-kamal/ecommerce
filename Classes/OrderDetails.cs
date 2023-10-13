using e_commerce.Classes.datatable;
using e_commerce.models;
using Microsoft.Data.SqlClient;

namespace e_commerce.Classes
{
    public class OrderDetails : Imodel
    {
        public models.Order order { get; set; }
        public orderDetails _orderDetails { get; set; }

        public void add(SqlDataReader sdr)
        {
            order = new models.Order();
            order.add(sdr);
            _orderDetails = new models.orderDetails();
            _orderDetails.add(sdr);

        }
    }
}
