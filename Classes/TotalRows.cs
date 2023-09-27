using e_commerce.models;
using Microsoft.Data.SqlClient;

namespace e_commerce.Classes
{
    public class TotalRows : Imodel
    {
        public int c { get; set; }



        public void add(SqlDataReader sdr)
        {
            c = Convert.ToInt32(sdr["c"]);
        }
    }
}
