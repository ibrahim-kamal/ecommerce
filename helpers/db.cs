using e_commerce.models;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace e_commerce.helpers
{
    public class db
    {
        public static db _db = null;

        public String last_query = "";

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


        public List<T> SetCommand<T>(string query,
        CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null) where T : Imodel, new()
        {
            List<T> result = new List<T>();
            Console.WriteLine(query);
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.CommandType = commandType;
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    T t = new T();
                    t.add(sdr);
                    result.Add(t);
                }
            }

            return result;
        }


        public int insert(string query,
        CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null) 
        {
            SqlCommand cmd = new SqlCommand(query + ";SELECT SCOPE_IDENTITY();", _connection);
            Console.WriteLine("inserted Query => " + query + ";SELECT SCOPE_IDENTITY();");
            cmd.CommandType = commandType;
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }


            try
            {
                //cmd.ExecuteNonQuery();
                //return (int)cmd.ExecuteScalar();
                
                object returnObj = cmd.ExecuteScalar();

                
                    
                return Convert.ToInt32(returnObj);
            }
            catch (SqlException e)
            {
               return 0;
            }

        }

        public int update(string query,
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


            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (SqlException e)
            {
                return 0;
            }

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
