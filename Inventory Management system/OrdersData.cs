using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Inventory_Management_system
{
    internal class OrdersData
    {
        SqlConnection connect
            = new SqlConnection("Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public int ID { get; set; }
        public string CID { set; get; }
        public string PID { set; get; }
        public string PName { set; get; }
        public string Category { set; get; }
        public string OrigPrice { set; get; }
        public string QTY { set; get; }
        public string TotalPrice { set; get; }
        public string Date { set; get; }

        public List<OrdersData> allOrdersData()
        {
            List<OrdersData> listData = new List<OrdersData>();
           // if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Close();
                    connect.Open();

                    int custID = 0;
                    string selectCustData = "SELECT MAX(customer_id) FROM orders1";

                    using(SqlCommand cmd = new SqlCommand(selectCustData, connect))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value) 
                        {
                            int temp = Convert.ToInt32(result);

                            if(temp == 0)
                            {
                                custID = 1;
                            }
                            else
                            {
                                custID = temp;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error ID");
                        }
                    }

                    string selectData = "SELECT * FROM orders1 WHERE customer_id = @cID";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cID", custID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrdersData oData = new OrdersData();

                            oData.ID = (int)reader["id"];
                            oData.CID = reader["customer_id"].ToString();
                            oData.PID = reader["prod_id"].ToString() ;
                            oData.PName = reader["prod_name"].ToString();
                            oData.Category = reader["category"].ToString() ;
                            oData.OrigPrice = reader["orig_price"].ToString();
                            oData.QTY = reader["qty"].ToString();
                            oData.TotalPrice = reader["total_price"].ToString();
                            oData.Date = reader["order_date"].ToString();

                            listData.Add(oData);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Connection failed: " + ex);   
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }
    }
}
