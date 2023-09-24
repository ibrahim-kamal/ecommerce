using Microsoft.Data.SqlClient;
using System.Data;

namespace e_commerce.helpers
{
    public class db
    {
        public static db _db = null; 
        private SqlConnection _connection { get; set; }
        private db(String ConnectionString)
        {

            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
        }


        public static void setDb(String ConnectionString)
        {

            if (_db == null)
            {

                _db = new db(ConnectionString);
            }
        }


        public SqlCommand SetCommand(string query,
        CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.CommandType = commandType;
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            return cmd;
        }



        private void OpenConnection(SqlConnection con)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        private void CloseConnection(SqlConnection con)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}
