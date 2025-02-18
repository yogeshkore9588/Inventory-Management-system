﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_system
{
    internal class CustomersData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public string CustomerID {  get; set; }
        public string TotalPrice { get; set; }
        public string Amount { get; set; }
        public string Change { get; set; }
        public string Date { get; set; }

        public List<CustomersData> allCustomers()
        {
            List<CustomersData> listData = new List<CustomersData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Close();
                    connect.Open();

                    string selectData = "SELECT * FROM customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CustomersData cData = new CustomersData();

                            cData.CustomerID = reader["customer_id"].ToString();
                            cData.TotalPrice = reader["total_price"].ToString();
                            cData.Amount = reader["amount"].ToString();
                            cData.Change = reader["change"].ToString();
                            cData.Date = reader["order_date"].ToString();

                            listData.Add(cData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed connection: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }

        public List<CustomersData> allTodayCustomers()
        {
            List<CustomersData> listData = new List<CustomersData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Close();
                    connect.Open();

                    DateTime today = DateTime.Today;

                    string selectData = "SELECT * FROM customers WHERE order_date = @date";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@date", today);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CustomersData cData = new CustomersData();

                            cData.CustomerID = reader["customer_id"].ToString();
                            cData.TotalPrice = reader["total_price"].ToString();
                            cData.Amount = reader["amount"].ToString();
                            cData.Change = reader["change"].ToString();
                            cData.Date = reader["order_date"].ToString();

                            listData.Add(cData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed connection: " + ex);
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
