using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExamWpf
{
    internal class DB
    {
        public SqlConnection connection;
        public DB() { 
            connection = new SqlConnection("Data Source=FEDOSDEKUDRILLE;Initial Catalog=ex;Integrated Security=True;TrustServerCertificate=True");
        }
        public string ReadData()
        {
            string str = "";
            try
            {
                connection.Open();
                SqlCommand cmd = new("select * from student", connection);
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    str += r.GetString(0) + ": " + r.GetInt32(1) + "\n";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return str;
        }
        public void WriteData()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new("update student set kurs = kurs + 1", connection);
                var r = cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
