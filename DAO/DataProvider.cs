using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Personal_PBL.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        private string strconn = @"Data Source=DESKTOP-D5R1A3V\SQLEXPRESS;Initial Catalog=QuanLyCafe;Integrated Security=True";

        private DataProvider() { }
        public static DataProvider Instance { get { if (instance == null) instance = new DataProvider(); return instance; }private set { instance = value; } }//Singleton Design Pattern

        public DataTable ExecuteQuery(string query,object[] parameter = null)
        {
            DataTable table = new DataTable();

            using(SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);

                if(parameter != null)
                {
                    string[] element = query.Split(' ');
                    int i = 0;
                    foreach(string item in element)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item,parameter[i]);
                            i++;
                        }
                    }
                }
                
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);
                conn.Close();
            }

            return table;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] element = query.Split(' ');
                    int i = 0;
                    foreach (string item in element)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                count = command.ExecuteNonQuery();
                conn.Close();
            }
            return count;
        }
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object obj = new object();

            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] element = query.Split(' ');
                    int i = 0;
                    foreach (string item in element)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                obj = command.ExecuteScalar();
                conn.Close();
            }

            return obj;
        }
    }
}
