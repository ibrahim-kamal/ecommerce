using Microsoft.Data.SqlClient;

namespace e_commerce.models
{
    public interface Imodel
    {
        public void add(SqlDataReader sdr);
    }
}
