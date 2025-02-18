using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Inventory_Management_system
{
    internal class AddProductsData
    {
        public int Id { get; set; } //0

        public string ProdID { get; set; } //1

        public string ProdName { get; set; } //2

        public string Category { get; set; } //3

        public string Price { get; set; } //4

        public string Stock { get; set; } //5

        public string ImagePath { get; set; } //6

        public string Status { get; set; } //7

        public string Date { get; set; } //8

        public List<AddProductsData> AllProductsData()
        {
            List<AddProductsData> listData = new List<AddProductsData>();

            SqlConnection connect
            = new SqlConnection("Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            {

                connect.Open();
                string selectDate = "SELECT * FROM products1";

                using (SqlCommand cmd = new SqlCommand(selectDate, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();
                        apData.Id = (int)reader["id"];
                        apData.ProdID = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = reader["stock"].ToString();
                        apData.ImagePath = reader["image_patch"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();
                        listData.Add(apData);
                    }
                }
            }
            return listData;
        }
        public List<AddProductsData> AllAvailableProducts()
        {
            List<AddProductsData> listData = new List<AddProductsData>();

            SqlConnection connect
                        = new SqlConnection(@"Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            {
                connect.Close();
                connect.Open();
                string selectDate = "SELECT * FROM products1 WHERE status = @status";

                using (SqlCommand cmd = new SqlCommand(selectDate, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Available");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();

                        apData.Id = (int)reader["id"];
                        apData.ProdID = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = reader["stock"].ToString();
                        apData.ImagePath = reader["image_patch"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();

                        listData.Add(apData);
                    }
                }
            }
            return listData;
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}
