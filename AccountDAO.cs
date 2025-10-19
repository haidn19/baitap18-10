using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ngay18_10
{
    public class AccountDAO
    {
        private string connStr = "server=localhost;user=root;password=duongchidung34@;database=testdb;";

        public bool Login(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM accounts WHERE username=@user AND password=@pass";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool Register(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // Hiển thị database đang kết nối để tránh nhầm
                MySqlCommand cmdDb = new MySqlCommand("SELECT DATABASE();", conn);
                MessageBox.Show("DEBUG: Connected database = " + cmdDb.ExecuteScalar().ToString());

                // 1️⃣ Kiểm tra username đã tồn tại chưa
                string check = "SELECT COUNT(*) FROM accounts WHERE username=@user";
                MySqlCommand cmdCheck = new MySqlCommand(check, conn);
                cmdCheck.Parameters.AddWithValue("@user", username);

                int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());
                MessageBox.Show($"DEBUG: Username = '{username}' exists count = {exists}");

                if (exists > 0)
                {
                    MessageBox.Show("Username đã tồn tại, không thêm.");
                    return false;
                }

                // 2️⃣ Thêm mới với active = 1
                string insert = "INSERT INTO accounts(username,password,active) VALUES(@user,@pass,1)";
                MySqlCommand cmd = new MySqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);

                int rows = cmd.ExecuteNonQuery();
                MessageBox.Show($"DEBUG: Rows affected = {rows}, active=1");

                return rows > 0;
            }
        }


        public bool ChangePassword(string username, string newPassword)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string update = "UPDATE accounts SET password=@pass WHERE username=@user";
                MySqlCommand cmd = new MySqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@pass", newPassword);
                cmd.Parameters.AddWithValue("@user", username);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int DeleteInactiveAccounts()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string delete = "DELETE FROM accounts WHERE active=0";
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
