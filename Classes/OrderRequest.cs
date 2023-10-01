
using e_commerce.Classes.datatable;
using e_commerce.models;
using Microsoft.Data.SqlClient;

namespace e_commerce.Classes
{
    public class OrderRequest
    {
        public models.Order order { get; set; }
        public Dictionary<int,orderDetails> orderDetails { get; set; }

    }
}
