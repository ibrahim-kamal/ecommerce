using e_commerce.Classes.datatable;
using e_commerce.helpers;
using e_commerce.Repostories;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace e_commerce.models
{
    public class Order : Imodel
    {

        public int orderId { get; set; }
        public int Fk_customerId { get; set; }
        public int Fk_userId { get; set; }

        public double orderSubTotal { get; set; }
        public double orderTotal { get; set; }
        public double orderDiscount { get; set; }
        public double OrderVat { get; set; }

        public String orderStatus { get; set; }
        public String address { get; set; }

        public Order() { 

        }
        public Order(Order order)
        {
            orderId = order.orderId;
            Fk_customerId = order.Fk_customerId;
            Fk_userId = order.Fk_userId;
            orderSubTotal = order.orderSubTotal;
            orderTotal = order.orderTotal;
            orderDiscount = order.orderDiscount;
            OrderVat = order.OrderVat;
            orderStatus = order.orderStatus;
            address = order.address;

        }

        public void add(SqlDataReader sdr) {
            orderId = Convert.ToInt32(sdr["orderId"]);
            Fk_customerId = Convert.ToInt32(sdr["Fk_customerId"]);
            Fk_userId =  Convert.ToInt32(sdr["Fk_userId"]);

            orderSubTotal  = Convert.ToDouble(sdr["orderSubTotal"]);
            orderTotal = Convert.ToDouble(sdr["orderTotal"]);
            orderDiscount = Convert.ToDouble(sdr["orderDiscount"]);
            OrderVat = Convert.ToDouble(sdr["OrderVat"]);
            orderStatus = sdr["orderStatus"].ToString();
            address = sdr["address"].ToString();

        }

    }
}
