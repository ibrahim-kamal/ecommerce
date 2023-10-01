using Azure;
using e_commerce.Classes;
using e_commerce.helpers;
using e_commerce.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Packaging.PackagingConstants;

namespace e_commerce.Repostories
{
    public class OrderDetailsRepostories
    {


        public int create(orderDetails orderDetails) {
            int id = db._db.insert("insert into orderDetails" +
                "" +
                " (Fk_orderId ," +
                " FK_productId ," +
                " orderDetailsQty ," +
                " orderDetailsPrice ," +
                " orderDetailsTotal ) " +
                "values" +
                "(" +
                "'" + orderDetails.Fk_orderId + "'," +
                "'" + orderDetails.FK_productId + "'," +
                "'" + orderDetails.orderDetailsQty + "'," +
                "'" + orderDetails.orderDetailsPrice + "'," +
                "" + orderDetails.orderDetailsTotal + ")");
            return id;
        }




        public void delete(int Id)
        {

            db._db.update("delete from Images where orderDetails = " + Id);

            
        }

    }
}
