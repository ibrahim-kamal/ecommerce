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
    public class Images
    {


        public int create(Image image) {
            int id = db._db.insert("insert into Images" +
                "(imageUrl , Fk_ProductId ) " +
                "values" +
                "('" + image.imageUrl + "'," + image.Fk_ProductId + ")");
            return id;
        }

      


        public void delete(int Id)
        {

            db._db.update("delete from Images where imageId = " + Id);

            
        }

    }
}
