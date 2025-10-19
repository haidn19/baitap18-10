using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ngay18_10
{
    public class DataHelper
    {
        private string connStr = "server=127.0.0.1;port=3306;database=testdb;user=root;password=duongchidung34@;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connStr);
        }

        public DataTable ExecuteQuery(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
