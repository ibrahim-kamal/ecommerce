using e_commerce.Classes;
using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.models
{
    public class orderDetails : Imodel
    {
        public int orderDetailsId { get; set; }
        public int Fk_orderId { get; set; }
        public int FK_productId { get; set; }
        public int orderDetailsQty { get; set; }

        public double orderDetailsPrice { get; set; }
        public double orderDetailsTotal { get; set; }

        public Product product { get; set; }

        public orderDetails() { }
        public orderDetails(orderDetails _orderDetails) {
            orderDetailsId = _orderDetails.orderDetailsId;
            Fk_orderId = _orderDetails.Fk_orderId ;
            FK_productId = _orderDetails.FK_productId ;
            orderDetailsQty = _orderDetails.orderDetailsQty ;
            orderDetailsPrice = _orderDetails.orderDetailsPrice ;
            orderDetailsTotal = _orderDetails.orderDetailsTotal ;
            product = new Product(_orderDetails.product);

        }
        public void add(SqlDataReader sdr) {
            orderDetailsId = Convert.ToInt32(sdr["orderDetailsId"]);
            Fk_orderId = Convert.ToInt32(sdr["Fk_orderId"]);
            FK_productId = Convert.ToInt32(sdr["FK_productId"]);
            orderDetailsQty  = Convert.ToInt32(sdr["orderDetailsQty"]);

            orderDetailsPrice  = Convert.ToDouble(sdr["orderDetailsPrice"]);
            orderDetailsTotal = Convert.ToDouble(sdr["orderDetailsTotal"]);
            product = new Product();

            product.add(sdr);

        }

    }
}
